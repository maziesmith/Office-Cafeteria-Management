using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Threading;
using Cafeteria.CoreLibs.Abstractions;
using Cafeteria.CoreLibs.DomainModel;
using Cafeteria.CoreLibs.Services;
using ReactiveWebsocket.Public;
using Syncfusion.UI.Xaml.Kanban;

namespace CafeteriaManagement.StaffApp.ViewModel
{
    public class MainPageViewModel
    {
        private readonly IFoodRepository _foodRepository = new FoodRepository();
        private readonly Random _random = new Random();
        public ObservableCollection<KanbanModel> Orders { get; set; }
        private readonly Dispatcher _dispatcher;
        private StringWebsocketClient _websocket;
        private const string ServerUri = "ws://cafe-server.azurewebsites.net/ReactiveWebSocketServer/staff";

        public MainPageViewModel()
        {
            Orders = new ObservableCollection<KanbanModel>();
            _dispatcher = Dispatcher.CurrentDispatcher;
            ListenForOrders();
            AddDummyOrder();
        }

        private void AddDummyOrder()
        {
            var orderDetails = GetOrderDetails("10", "6");
            _dispatcher.Invoke(() => Orders.Add(orderDetails));
        }

        private void ListenForOrders()
        {
            _websocket = new StringWebsocketClient();
            _websocket.ConnectAsync(new Uri(ServerUri), CancellationToken.None)
                .ContinueWith(task =>
                {
                    if (task.Result)
                    {
                        _websocket.GetObservable(s => true).Subscribe(ids =>
                        {
                            ExtractId(ids, out var orderId, out var foodId);
                            var orderDetails = GetOrderDetails(orderId, foodId);
                            _dispatcher.Invoke(() => Orders.Add(orderDetails));
                        });
                    }
                });
        }

        private static void ExtractId(string ids, out string orderId, out string foodId)
        {
            var parts = ids.Split(',');
            orderId = parts[0];
            foodId = parts[1];
        }

        public void ItemMoved(KanbanDragEndEventArgs args)
        {
            var model = (KanbanModel)args.SelectedCard.Content;
            if (args.TargetKey.ToString() != OrderStatus.Prepared.ToString()) return;
            var message = model.Tags[0] + "," + model.Tags[1];
            _websocket.GetResponse(message, s => true);
        }

        private KanbanModel GetOrderDetails(string orderId, string foodId)
        {
            var foodItem = _foodRepository.GetFoodInfo(int.Parse(foodId));
            return new KanbanModel
            {
                Title = foodItem.Details.Name,
                Description = $"Order Id: {orderId}",
                Tags = new[] { orderId, foodId },
                Category = OrderStatus.Received.ToString(),
                ImageURL = new Uri("Images/" + foodItem.Id + ".png", UriKind.RelativeOrAbsolute),
        };
    }
}
}
