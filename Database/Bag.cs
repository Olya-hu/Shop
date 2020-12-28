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
        [Column("product_id", TypeName = "text")]
        public string ProductId { get; set; }
        [Column("size", TypeName = "text")]
        public string Size { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Bag")]
        public virtual User User { get; set; }
    }
}
