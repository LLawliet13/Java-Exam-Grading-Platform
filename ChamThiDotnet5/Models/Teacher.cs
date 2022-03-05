using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChamThiDotnet5.Models
{
    [Table("Teachers")]
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength (100)]
        [Required]
        public string TeacherName { get; set; }
        [Required]
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        public List<Class> Classes { get; set; }

    }
}
