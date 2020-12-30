using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Enums;

namespace Database
{
    [Table("order")]
    public partial class Order
    {
        public Order()
        {
            ProductOrder = new HashSet<ProductOrder>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("shipping_id")]
        public int ShippingId { get; set; }
        [Column("date", TypeName = "timestamp")]
        public DateTime Date { get; set; }
        [Column("payment", TypeName = "decimal(10,2)")]
        public decimal Payment { get; set; }
        [Required]
        [Column("status", TypeName = "enum('Processing','Shipping','Delivered')")]
        public Status Status { get; set; }

        [ForeignKey(nameof(ShippingId))]
        [InverseProperty("Order")]
        public virtual Shipping Shipping { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Order")]
        public virtual User User { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
    }
}
