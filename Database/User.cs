using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Enums;

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
        public string Password { get; set; }
        [Required]
        [Column("email", TypeName = "varchar(45)")]
        public string Email { get; set; }
        [Column("country", TypeName = "enum('Russia','USA','UK','France')")]
        public Country? Country { get; set; }
        [Column("address", TypeName = "varchar(64)")]
        public string Address { get; set; }
        [Column("postcode", TypeName = "varchar(12)")]
        public string Postcode { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
