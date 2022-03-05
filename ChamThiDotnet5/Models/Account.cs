using System.ComponentModel.DataAnnotations;

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
        [Required]
        public int AccountTypeId { get; set; }


    }
}
