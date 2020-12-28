using System.ComponentModel.DataAnnotations;
using Database.Enums;

namespace Services.Orders.Requests
{
    public class OrderRequest
    {
        [Required]
        public int ShippingId { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public int[] ProductIds { get; set; }
        
        [Required]
        public Size[] Sizes { get; set; }
    }
}
