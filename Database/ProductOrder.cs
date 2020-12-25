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

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("ProductOrder")]
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductOrder")]
        public virtual Product Product { get; set; }
    }
}
