using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamThiDotnet5.Models
{
    [Table("Teachers")]
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [StringLength (100)]
        public string TeacherName { get; set; }
        public int AccountId { get; set; }

    }
}
