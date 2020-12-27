using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.DbConnection;

namespace Shop.Controllers
{
    [Route("admin")]
    public class AdminsController : BaseDbController
    {
        private readonly ShopContext _context;

        public AdminsController(IShopConnection shopConnection) : base(shopConnection)
        {
            _context = shopConnection.Context;
        }
        
        [HttpGet]
        [Route("list")]
        //[Authorize(Roles = "admin")]
        public async Task<List<Admin>> Get()
        {
            return await _context.Admin.ToListAsync();
        }

        [HttpPost]
        [Route("add")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Add([FromBody] Admin admin)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var entity = await _context.Admin.AddAsync(admin);
            await _context.SaveChangesAsync();
            return new EmptyResult();
        }

        [HttpDelete]
        [Route("delete")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Remove(int adminId)
        {
            _context.Admin.Remove(await _context.Admin.FindAsync(adminId));
            await _context.SaveChangesAsync();
            return new EmptyResult();
        }
    }
}
