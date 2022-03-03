using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamThiDotnet5.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Studentname { get; set; }
        public float Score { get; set; }
        public int ClassId { get; set; }

        public int AccountId { get; set; }
        public int ExamId { get; set; }
    }
}
