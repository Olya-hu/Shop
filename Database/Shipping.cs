using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    [Table("shipping")]
    public partial class Shipping
    {
        public Shipping()
        {
            Order = new HashSet<Order>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(16)")]
        public string Name { get; set; }
        [Required]
        [Column("company", TypeName = "varchar(16)")]
        public string Company { get; set; }
        [Column("price", TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        [Column("description", TypeName = "mediumtext")]
        public string Description { get; set; }

        [InverseProperty("Shipping")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
