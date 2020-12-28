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
using Shop.Models.Requests.Admins;

namespace Shop.Controllers
{
    [Route("admin")]
    [Authorize(Roles = "admin")]
    public class AdminsController : BaseDbController
    {
        private readonly ShopContext _dbContext;

        public AdminsController(IShopConnection shopConnection) : base(shopConnection)
        {
            _dbContext = shopConnection.Context;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return HttpContext.User.Identity.IsAuthenticated ? null : RedirectToAction("Login");
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();
            var admin = await _dbContext.Admin.FirstOrDefaultAsync(a => a.Username == request.Username && a.Password == request.Password);
            if (admin == null)
                ModelState.AddModelError("Некорректные аутентификационные данные", "Неверные логин и/или пароль");
            await Authenticate(admin);
            return RedirectToAction("Index");
        }

        private async Task Authenticate(Admin admin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim(ClaimTypes.Role, "admin")
            };
            var identity = new ClaimsIdentity(claims, "AdminCookie", ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddDays(1)
                });
        }

        [HttpDelete]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AdminAuthentication");
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        [Route("list")]
        public async Task<List<Admin>> GetAdminsList()
        {
            return await _dbContext.Admin.ToListAsync();
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] string username)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            await _dbContext.Admin.AddAsync(new Admin
            {
                Username = username,
                Password = RandomPassword()
            });
            await _dbContext.SaveChangesAsync();
            return new EmptyResult();
        }

        private static string RandomPassword()
        {
            var r = new Random();
            var password = "";
            for (var i = 0; i < 16; i++)
            {
                var c = (char) r.Next(33, 125);
                if (char.IsLetterOrDigit(c))
                    password += c;
            }

            return password;
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(int adminId)
        {
            _dbContext.Admin.Remove(await _dbContext.Admin.FindAsync(adminId));
            await _dbContext.SaveChangesAsync();
            return new EmptyResult();
        }
    }
}
