namespace CourseManagement.DTOs
{
    public class GetStudentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();       
    }
}
