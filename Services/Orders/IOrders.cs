using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Services.Orders.Requests;

namespace Services.Orders
{
    public interface IOrders
    {
        Task<IEnumerable<Shipping>> GetShippings();
        Task Make(OrderRequest request);
        Task AddToBag(ProductSize ps, int userId);
        Task<IEnumerable<Bag>> GetBagForUser(int userId);
    }
}
