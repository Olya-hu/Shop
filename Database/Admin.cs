using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    [Table("admin")]
    public partial class Admin
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("username", TypeName = "varchar(16)")]
        public string Username { get; set; }
        [Required]
        [Column("password", TypeName = "varchar(16)")]
        public string Password { get; set; }
    }
}
