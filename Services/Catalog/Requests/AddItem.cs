using System.ComponentModel.DataAnnotations;
using Database.Enums;

namespace Services.Catalog.Requests
{
    public class AddItem
    {
        [Required]
        public string VendorCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public Brand Brand { get; set; }
        [Required]
        public Color Color { get; set; }
        [Required]
        public string[] Sizes { get; set; }
        [Required]
        public int[] Quantities { get; set; }
    }
}