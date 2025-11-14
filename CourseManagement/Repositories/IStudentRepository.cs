using CourseManagement.Models;

namespace CourseManagement.Repositories
{
    public interface IStudentRepository
    {
        Task<IQueryable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student> AddStudentAsync(Student student);
        Task<bool> UpdateStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(int id);

        Task<IEnumerable<Course>> GetStudentCoursesAsync(int studentId);
    }
}
