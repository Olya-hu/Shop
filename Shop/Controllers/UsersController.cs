using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    [Route("")]
    public class UsersController : ControllerBase
    {
        private readonly ShopContext _dbContext;

        public UsersController(ShopContext dbContext)
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
    }
}
