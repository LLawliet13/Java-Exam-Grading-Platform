using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamThiDotnet5.Models
{
    [Table("Exams")]
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        public string Examname { get; set; }
        public string Detail { get; set; }
    }
}
