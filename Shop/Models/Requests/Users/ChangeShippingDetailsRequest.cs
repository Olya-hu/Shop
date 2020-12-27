using System.ComponentModel.DataAnnotations;
using Database.Enums;

namespace Shop.Models.Requests.Users
{
    public class ChangeShippingDetailsRequest
    {
        [Required(ErrorMessage = "Не указана страна")]
        public Country Country { get; set; }
        [Required(ErrorMessage = "Адрес не указан")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Почтовый индекс не указан")]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }
    }
}
