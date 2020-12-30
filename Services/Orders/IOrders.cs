using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Database.Enums;

namespace Services.Orders
{
    public interface IOrders
    {
        Task<IEnumerable<Shipping>> GetShippings();
        Task Make(int userId, int shippingId);
        Task<IEnumerable<Order>> GetAllOrders(int orderId);
        Task<List<Order>> GetOrdersForUser(int userId);
        Task<Order> GetOrder(int orderId);
        Task ChangeStatus(int orderId, Status status);
    }
}
