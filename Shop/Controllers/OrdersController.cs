using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Mvc;
using Services.Orders;
using Services.Orders.Requests;

namespace Shop.Controllers
{
    [Route("order")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrders _orders;

        public OrdersController(IOrders orders)
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
        //[Authorize(Roles = "user")]
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
