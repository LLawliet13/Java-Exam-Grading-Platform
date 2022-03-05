using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamThiDotnet5.Models
{
    [Table("Classes")]
    public class Class
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Classname { get; set; }
        [Required]
        public string TeacherID { get; set; }
    }
}
