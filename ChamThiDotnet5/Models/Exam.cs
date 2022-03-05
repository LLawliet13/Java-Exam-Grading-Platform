using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamThiDotnet5.Models
{
    [Table("Exams")]
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Examname { get; set; }
        [Required]
        public string Detail { get; set; }

        public List<Student> Students { get; set; }
    }
}
