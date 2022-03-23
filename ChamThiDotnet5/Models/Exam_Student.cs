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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        [Required]

        public int StudentId { get; set; }
        [Required]

        public int ExamId { get; set; }

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

        public string SubmittedFolder { get; set; }
        public float? Score { get; set; }
    }
}
