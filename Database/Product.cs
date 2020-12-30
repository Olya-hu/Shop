using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Enums;

namespace Database
{
    [Table("product")]
    public partial class Product
    {
        public Product()
        {
            ProductSize = new HashSet<ProductSize>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("vendor_code", TypeName = "varchar(16)")]
        public string VendorCode { get; set; }
        [Required]
        [Column("name", TypeName = "tinytext")]
        public string Name { get; set; }
        [Column("price", TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        [Column("description", TypeName = "text")]
        public string Description { get; set; }
        [Required]
        [Column("gender", TypeName = "enum('Male','Female','Unisex')")]
        public Gender Gender { get; set; }
        [Required]
        [Column("category", TypeName = "enum('Clothes','Footwear','Sportswear')")]
        public Category Category { get; set; }
        [Required]
        [Column("brand", TypeName = "enum('Adidas','Nike','Puma','Supreme','Asos','Bershka')")]
        public Brand Brand { get; set; }
        [Required]
        [Column("color", TypeName = "enum('White','Black','BlackWhite','Grey','Red','Orange','Yellow','Green','Cyan','Blue','Purple','Pink','Multicolor')")]
        public Color Color { get; set; }
        [Required]
        [Column("image", TypeName = "mediumblob")]
        public byte[] Image { get; set; }

        [InverseProperty("Product")]
        public virtual ICollection<ProductSize> ProductSize { get; set; }
    }
}
