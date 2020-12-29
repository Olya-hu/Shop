using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    [Table("product_order")]
    public partial class ProductOrder
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Key]
        [Column("product_size", TypeName = "enum('36','37','38','39','40','41','42','43','44','45','46','47','48','50','52','54','56','XS','S','M','L','XL','2XL','3XL','4XL','5XL')")]
        public string ProductSize { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("ProductOrder")]
        public virtual Order Order { get; set; }
        [ForeignKey("ProductId,ProductSize")]
        [InverseProperty("ProductOrder")]
        public virtual ProductSize Product { get; set; }
    }
}
