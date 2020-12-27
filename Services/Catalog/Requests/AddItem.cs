﻿using System.ComponentModel.DataAnnotations;
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
        public Size[] Sizes { get; set; }
        [Required]
        public short[] Quantities { get; set; }

        private int SizesLength => Sizes.Length;
        
        [Compare("SizesLength", ErrorMessage = "Не соответствие между размерами и количеством")]
        private int QuantitiesLength => Quantities.Length;
    }
}