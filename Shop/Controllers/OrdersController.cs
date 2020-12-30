using System.Linq;
using System.Threading.Tasks;
using Database.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DbConnection;
using Services.Orders;

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
        [Authorize(Roles = "user")]
        [Route("shippingSelection")]
        public async Task<IActionResult> ShippingSelection()
        {
            return View(await _orders.GetShippings());
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Index([FromForm] int shippingId)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            if (!int.TryParse(HttpContext.User.FindFirst(x => x.Type == "Id").Value, out var userId))
                return Unauthorized();
            if ((await ShopConnection.Context.User.FindAsync(userId)).Postcode == null)
                return RedirectToAction("ChangeShippingDetails", "Users");
            await _orders.Make(userId, shippingId);
            return RedirectToAction("My");
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        [Route("my")]
        public async Task<IActionResult> My(int orderId = -1)
        {
            if (!int.TryParse(HttpContext.User.FindFirst(x => x.Type == "Id").Value, out var userId))
                return Unauthorized();
            if (orderId == -1)
                return View(await _orders.GetOrdersForUser(userId));
            var order = await _orders.GetOrder(orderId);
            if (order.UserId != userId)
                return Unauthorized();
            return View("Order", order);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("all")]
        public async Task<IActionResult> All(bool list = true, int orderId = -1)
        {
            return list ? View(await _orders.GetAllOrders(orderId)) : View("Order", await _orders.GetOrder(orderId));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("changeOrderStatus")]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, Status status)
        {
            await _orders.ChangeStatus(orderId, status);
            return RedirectToAction("All");
        }
    }
}
