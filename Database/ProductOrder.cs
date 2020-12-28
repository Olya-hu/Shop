using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Enums;

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
        [Column("product_size", TypeName = "enum('36','38','40','42','44','46','48','50','52','54','56')")]
        public Size ProductSize { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("ProductOrder")]
        public virtual Order Order { get; set; }
        [ForeignKey("ProductId,ProductSize")]
        [InverseProperty("ProductOrder")]
        public virtual ProductSize Product { get; set; }
    }
}
