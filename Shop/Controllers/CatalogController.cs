using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Catalog;
using Services.Catalog.Requests;
using Services.DbConnection;

namespace Shop.Controllers
{
    [Route("catalog")]
    public class CatalogController : BaseDbController
    {
        private readonly ICatalog _catalog;

        public CatalogController(ICatalog catalog, IShopConnection connection) : base(connection)
        {
            _catalog = catalog;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] CatalogFilters filters)
        {
            return ModelState.IsValid ? new ObjectResult(await _catalog.GetWithFilters(filters)) : ValidationProblem();
        }

        [HttpGet]
        [Route("{productId:int}/sizes")]
        public async Task<Dictionary<string, int>> GetSizesFor(int productId)
        {
            return await _catalog.GetSizesFor(productId);
        }
        
        [HttpGet]
        [Route("addItem")]
        [Authorize(Roles = "admin")]
        public IActionResult AddItem()
        {
            return View("~/Views/Admins/AddItem.cshtml");
        }

        [HttpPost]
        [Route("addItem")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddItem([FromForm] AddItem request, IFormFile file)
        {
            if (!ModelState.IsValid || file == null)
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
    }
}
