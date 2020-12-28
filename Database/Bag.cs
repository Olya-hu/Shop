using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Enums;

namespace Database
{
    [Table("bag")]
    public partial class Bag
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Key]
        [Column("size", TypeName = "enum('36','38','40','42','44','46','48','50','52','54','56')")]
        public Size Size { get; set; }

        [ForeignKey("ProductId,Size")]
        [InverseProperty("Bag")]
        public virtual ProductSize ProductSize { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Bag")]
        public virtual User User { get; set; }
    }
}
