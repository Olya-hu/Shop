using System.Collections.Generic;
using System.Threading.Tasks;
using Database;

namespace Services.Orders
{
    public interface IOrders
    {
        Task<IEnumerable<Shipping>> GetShippings();
        Task Make(int userId, int shippingId);
        Task<List<Order>> GetOrdersForUser(int userId);
        Task<Order> GetOrder(int userId, int orderId);
    }
}
