using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.DbConnection;
using Shop.Models.Requests.Users;

namespace Shop.Controllers
{
    [Route("user")]
    public class UsersController : BaseDbController
    {
        private readonly ShopContext _dbContext;

        public UsersController(IShopConnection connection) : base(connection)
        {
            _dbContext = connection.Context;
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<IActionResult> SignUp([FromForm] SignUpRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            var user = (await _dbContext.User.AddAsync(new User()
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            })).Entity;
            await _dbContext.SaveChangesAsync();
            await Authenticate(user);
            return RedirectToAction("ChangeShippingDetails");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return new EmptyResult();
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            var user = await _dbContext.User.FirstOrDefaultAsync(u =>
                (u.Username == request.Username || u.Email == request.Username) && u.Password == request.Password);
            if (user == null)
                ModelState.AddModelError("Некорректные аутентификационные данные", "Неверные логин и/или пароль");
            await Authenticate(user);
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString(), ClaimValueTypes.Integer),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "user")
            };
            if (user.Country != null)
                claims.Add(new Claim("Country", user.Country.ToString()));
            if (!string.IsNullOrEmpty(user.Address))
                claims.Add(new Claim("Address", user.Address));
            if (!string.IsNullOrEmpty(user.Postcode))
                claims.Add(new Claim(ClaimTypes.PostalCode, user.Postcode));
            var identity = new ClaimsIdentity(claims, "UserCookie", ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync("UserAuthentication", 
                principal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                });
        }

        private async Task SignOut()
        {
            await HttpContext.SignOutAsync("UserAuthentication");
        }
        
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await SignOut();
            return RedirectToAction("login");
        }

        [HttpPost]
        [Route("changeShippingDetails")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> ChangeShippingDetails([FromForm] ChangeShippingDetailsRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            if (!int.TryParse(HttpContext.User.FindFirst("Id").Value, out var id))
                return Unauthorized();

            var user = await _dbContext.User.FindAsync(id);
            if (user == null)
                return UnprocessableEntity();
            await SignOut();
            user.Country = request.Country;
            user.Address = request.Address;
            user.Postcode = request.Postcode;
            await _dbContext.SaveChangesAsync();
            await Authenticate(user);
            return RedirectToAction("Index", "Home");
        }
    }
}
