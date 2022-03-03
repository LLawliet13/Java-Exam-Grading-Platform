using System.ComponentModel.DataAnnotations;

namespace ChamThiDotnet5.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public string Classname { get; set; }
        public string TeacherID { get; set; }
    }
}
