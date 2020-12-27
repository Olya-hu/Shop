using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DbConnection;
using Services.Orders;
using Services.Orders.Requests;

namespace Shop.Controllers
{
    [Route("order")]
    public class OrdersController : BaseDbController
    {
        private readonly IOrders _orders;

        public OrdersController(IOrders orders, IShopConnection connection) : base(connection)
        {
            _orders = orders;
        }

        [HttpGet]
        [Route("getShippings")]
        public async Task<List<Shipping>> GetShippings()
        {
            return await _orders.GetShippings();
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Index([FromBody] OrderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                await _orders.Make(request);
            }
            catch (DBConcurrencyException)
            {
                return new StatusCodeResult(500);
            }

            return new EmptyResult();
        }
    }
}
