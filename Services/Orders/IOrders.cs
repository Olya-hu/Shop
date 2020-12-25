using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Services.Orders.Requests;

namespace Services.Orders
{
    public interface IOrders
    {
        Task<List<Shipping>> GetShippings();
        Task Make(OrderRequest request);
    }
}
