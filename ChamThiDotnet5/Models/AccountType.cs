using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamThiDotnet5.Models
{
    [Table("AccountTypes")]
    public class AccountType
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Typename { get; set; }
    }
}
