using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Catalog;
using Services.Catalog.Requests;
using Services.DbConnection;

namespace Shop.Controllers
{
    [Route("")]
    [Route("catalog")]
    public class CatalogController : BaseDbController
    {
        private readonly ICatalog _catalog;
        private readonly string[] _trustedExtensions = {".jpg", ".png"};

        public CatalogController(ICatalog catalog, IShopConnection connection) : base(connection)
        {
            _catalog = catalog;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] CatalogFilters filters)
        {
            return ModelState.IsValid ? View(await _catalog.GetWithFilters(filters)) : ValidationProblem();
        }

        [HttpGet]
        [Route("product")]
        public async Task<IActionResult> Product(int productId)
        {
            return View("Product", await _catalog.GetProduct(productId));
        }
        
        [HttpGet]
        [Route("addItem")]
        [Authorize(Roles = "admin")]
        public IActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        [Route("addItem")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddItem([FromForm] AddItem request, IFormFile file)
        {
            if (file == null)
                ModelState.AddModelError("File", "Файл не был получен");
            if (!_trustedExtensions.Contains(Path.GetExtension(file?.FileName)))
                ModelState.AddModelError("File","Был передан неверный тип файла");
            if (!ModelState.IsValid)
                return ValidationProblem();
            byte[] image = null;
            await using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                // Файл меньше 4МБ
                if (memoryStream.Length < 4194304)
                {
                    image = memoryStream.ToArray();
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }
            await _catalog.AddProduct(request, image);
            return RedirectToAction("AddItem");
        }
        
        [HttpPost]
        [Authorize(Roles = "user")]
        [Route("addToBag")]
        public async Task<IActionResult> AddToBag([FromForm] int productId, [FromForm] string size)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            if (!int.TryParse(HttpContext.User.FindFirst(x => x.Type == "Id").Value, out var userId))
                return Unauthorized();
            await _catalog.AddToBag(productId, size, userId);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Authorize(Roles = "user")]
        [Route("removeFromBag")]
        public async Task<IActionResult> RemoveFromBag([FromForm] int productId, [FromForm] string size)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            if (!int.TryParse(HttpContext.User.FindFirst(x => x.Type == "Id").Value, out var userId))
                return Unauthorized();
            await _catalog.RemoveFromBag(productId, size, userId);
            return RedirectToAction("GetBag", "Users");
        }
    }
}
