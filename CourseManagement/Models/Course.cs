using System.ComponentModel.DataAnnotations;


namespace CourseManagement.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [StringLength(maximumLength:100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(maximumLength:100)]
        public string Description { get; set; } = string.Empty;

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
