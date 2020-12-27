using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Requests.Users
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Не указано имя пользователя")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
    }
}
