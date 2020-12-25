using System.Collections.Generic;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shop.Controllers
{
    [Route("")]
    public class AccountsController : ControllerBase
    {
        private readonly ShopContext _dbContext;

        public AccountsController(ShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<IActionResult> SignUp([FromForm] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await _dbContext.User.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            // Авторизация
            return new EmptyResult();
        }

        [HttpGet]
        [Route("admin/list")]
        //[Authorize(Roles = "admin")]
        public async Task<List<Admin>> Get()
        {
            return await _dbContext.Admin.ToListAsync();
        }

        [HttpPost]
        [Route("admin/add")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Add([FromBody] Admin admin)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var entity = await _dbContext.Admin.AddAsync(admin);
            if (entity.State != EntityState.Added)
                return new StatusCodeResult(500);
            await _dbContext.SaveChangesAsync();
            return new EmptyResult();
        }

        [HttpDelete]
        [Route("admin/delete")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Remove(int adminId)
        {
            var entity = _dbContext.Admin.Remove(await _dbContext.Admin.FindAsync(adminId));
            if (entity.State != EntityState.Deleted)
                return new StatusCodeResult(500);
            await _dbContext.SaveChangesAsync();
            return new EmptyResult();
        }
    }
}
