using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamThiDotnet5.Models
{
    public class Account
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
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
