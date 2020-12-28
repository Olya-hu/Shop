using System.ComponentModel.DataAnnotations;
using Database;

namespace Services.Orders.Requests
{
    public class OrderRequest
    {
        [Required]
        public int ShippingId { get; set; }
        
        [Required]
        public int UserId { get; set; }
    }
}
