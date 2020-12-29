using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column("size", TypeName = "enum('36','37','38','39','40','41','42','43','44','45','46','47','48','50','52','54','56','XS','S','M','L','XL','2XL','3XL','4XL','5XL')")]
        public string Size { get; set; }

        [ForeignKey("ProductId,Size")]
        [InverseProperty("Bag")]
        public virtual ProductSize ProductSize { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Bag")]
        public virtual User User { get; set; }
    }
}
