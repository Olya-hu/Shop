using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Enums;

namespace Database
{
    [Table("product_size")]
    public partial class ProductSize
    {
        public ProductSize()
        {
            Bag = new HashSet<Bag>();
            ProductOrder = new HashSet<ProductOrder>();
        }

        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Key]
        [Column("size", TypeName = "enum('36','38','40','42','44','46','48','50','52','54','56')")]
        public Size Size { get; set; }
        [Column("quantity")]
        public short Quantity { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductSize")]
        public virtual Product Product { get; set; }
        [InverseProperty("ProductSize")]
        public virtual ICollection<Bag> Bag { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
    }
}
