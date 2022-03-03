using System.ComponentModel.DataAnnotations;

namespace ChamThiDotnet5.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int AccountTypeId { get; set; }


    }
}
