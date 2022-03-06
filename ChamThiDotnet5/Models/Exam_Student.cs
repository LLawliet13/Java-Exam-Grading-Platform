using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamThiDotnet5.Models
{
    [Table("Exam_Student")]
    public class Exam_Student
    {
        [Key]
        public int Id { get; set; } 

        [Required]

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        [Required]

        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }
        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public float? Score { get; set; }
    }
}
