using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    [Table("user")]
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("username", TypeName = "varchar(16)")]
        public string Username { get; set; }
        [Required]
        [Column("password", TypeName = "varchar(16)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Column("country", TypeName = "enum('Russia','USA','UK','France')")]
        public string Country { get; set; }
        [Column("address", TypeName = "varchar(64)")]
        public string Address { get; set; }
        [Column("postcode", TypeName = "varchar(12)")]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
