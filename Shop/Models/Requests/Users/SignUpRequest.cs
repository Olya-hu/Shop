using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Requests.Users
{
    public class SignUpRequest
    {
        [Required(ErrorMessage = "Не указано имя пользователя")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Не указан Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string SubmitPassword { get; set; }
    }
}
