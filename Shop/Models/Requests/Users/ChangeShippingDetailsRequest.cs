using System.ComponentModel.DataAnnotations;
using Database.Enums;

namespace Shop.Models.Requests.Users
{
    public class ChangeShippingDetailsRequest
    {
        [Required]
        public Country Country { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }
    }
}
