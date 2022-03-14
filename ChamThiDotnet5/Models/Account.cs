using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamThiDotnet5.Models
{
    public class Account
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username not empty")]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password not empty")]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email not empty")]
        [RegularExpression(pattern: @"^[\w\.]+@([\w-]+\.)+[\w-]{2,3}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        public int AccountTypeId { get; set; }

        [ForeignKey("AccountTypeId")]
        public AccountType AccountType { get; set; }
        //public List<Teacher> Teachers { get; set; }
        //public List<Student> Students { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual Student Student { get; set; }


    }
}
