using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamThiDotnet5.Models
{
    [Table("Exams")]
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Examname { get; set; }
        [Required]
        public string Detail { get; set; }

        public List<Exam_Student> Exam_Students { get; set; }
        
    }
}
