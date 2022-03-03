using System.ComponentModel.DataAnnotations;

namespace ChamThiDotnet5.Models
{
    public class AccountType
    {
        [Key]
        public int Id { get; set; }
        public string Typename { get; set; }
    }
}
