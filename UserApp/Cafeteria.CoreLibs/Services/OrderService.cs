using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cafeteria.CoreLibs.Abstractions;
using Cafeteria.CoreLibs.DomainModel;
using ReactiveWebsocket.Model;
using ReactiveWebsocket.Public;

namespace Cafeteria.CoreLibs.Services
{
    public class OrderService : IOrderService
    {
        private readonly StringWebsocketClient _websocketClient;
        private readonly Random _random = new Random();
        private bool _isConnecting;
        private const string Uri = "ws://cafe-server.azurewebsites.net/ReactiveWebSocketServer/user";

        public OrderService()
        {
            _websocketClient = new StringWebsocketClient();
        }

        private Task<bool> EnsureConnected()
        {
            var tcs = new TaskCompletionSource<bool>();
            _websocketClient.StatusStream.Subscribe(status =>
            {
                if (status.ConnectionState != ConnectionState.Connected)
                {
                    if (_isConnecting) return;
                    _isConnecting = true;
                    Connect().ContinueWith(task =>
                    {
                        _isConnecting = false;
                        tcs.TrySetResult(task.Result);
                    });
                }
                else
                {
                    tcs.SetResult(true);
                }
            });
            return tcs.Task;
        }

        private async Task<bool> Connect()
        {
            var success = await _websocketClient.ConnectAsync(new Uri(Uri), CancellationToken.None);
            _isConnecting = false;
            return success;
        }

        public Task<bool> SendOrder(Order order)
        {
            //Should come from server
            order.OrderId = _random.Next(10, 100);
            order.OrderStatus = OrderStatus.Received;
            return EnsureConnected().ContinueWith(task =>
            {
                if (!task.Result) return false;
                var message = order.OrderId.ToString() + "," + order.FoodId.ToString();
                _websocketClient.GetResponse(message, s => true);
                return true;
            });
        }

        public IObservable<OrderStatus> GetOrderStatusObservable(int orderId)
        {
            return _websocketClient.GetObservable(message =>
            {
                ExtractId(message, out var orderIdReceived, out var foodId);
                return int.Parse(orderIdReceived).Equals(orderId);
            }).Select(s => OrderStatus.Prepared);
        }

        private static void ExtractId(string ids, out string orderId, out string foodId)
        {
            var parts = ids.Split(',');
            if (parts.Length != 2)
            {
                orderId = "0";
                foodId = "0";
                return;
            }
            orderId = parts[0];
            foodId = parts[1];
        }

        public Task<IEnumerable<Order>> GetPastOrders()
        {
            var tcs = new TaskCompletionSource<IEnumerable<Order>>();
            var orders = new List<Order>();
            orders.AddRange(CreateOrders(20, 6));
            orders.AddRange(CreateOrders(16, 12));
            orders.AddRange(CreateOrders(12, 1));
            orders.AddRange(CreateOrders(8, 4));
            orders.AddRange(CreateOrders(5, 10));
            tcs.SetResult(orders);
            return tcs.Task;
        }

        private static IEnumerable<Order> CreateOrders(int count, int foodId)
        {
            var orders = new List<Order>();
            for (var i = 0; i < count; i++)
            {
                orders.Add(new Order
                {
                    FoodId = foodId
                });
            }
            return orders;
        }
    }
}