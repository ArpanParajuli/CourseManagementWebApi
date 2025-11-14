using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace CourseManagement.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [StringLength(maximumLength:100)]
        public string FullName { get; set; } = string.Empty;

        [StringLength(maximumLength:100)]
        public string Email {get; set;} = string.Empty;

        [JsonIgnore]
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
