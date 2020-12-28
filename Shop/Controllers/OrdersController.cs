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
        public async Task<IEnumerable<Shipping>> GetShippings()
        {
            return await _orders.GetShippings();
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Index([FromBody] OrderRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            await _orders.Make(request);
            return new EmptyResult();
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        [Route("addToBag")]
        public async Task<IActionResult> AddToBag([FromBody] ProductSize ps)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            if (!int.TryParse(HttpContext.User.FindFirst(x => x.Type == "Id").Value, out var userId))
                return Unauthorized();
            if (ps.Quantity <= 0)
                ModelState.AddModelError("Quantity", "Товара нет на складе");
            else
                await _orders.AddToBag(ps, userId);
            return new EmptyResult();
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        [Route("bag")]
        public async Task<IActionResult> GetBag()
        {
            if (!int.TryParse(HttpContext.User.FindFirst(x => x.Type == "Id").Value, out var userId))
                return Unauthorized();
            return new ObjectResult(await _orders.GetBagForUser(userId));
        }
    }
}
