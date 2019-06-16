using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cafeteria.CoreLibs.DomainModel;

namespace Cafeteria.CoreLibs.Abstractions
{
    public interface IOrderService
    {
        Task<bool> SendOrder(Order order);

        IObservable<OrderStatus> GetOrderStatusObservable(int orderId);

        Task<IEnumerable<Order>> GetPastOrders();
    }
}