using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Mvc;
using Shop.Models.Requests.Users;

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
        public async Task<IActionResult> SignUp([FromForm] SignUpRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await _dbContext.User.AddAsync(new User()
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            });
            await _dbContext.SaveChangesAsync();
            // Авторизация
            return new EmptyResult();
        }

        [HttpPost]
        [Route("changeShippingDetails")]
        public async Task<IActionResult> ChangeShippingDetails([FromForm] ChangeShippingDetailsRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            return new EmptyResult();
        }
    }
}
