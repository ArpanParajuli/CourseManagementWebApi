using CourseManagement.Models;

namespace CourseManagement.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(int id);
        Task<bool> AddCourseAsync(Course course);
        Task<bool> UpdateCourseAsync(Course course);
        Task<bool> DeleteCourseAsync(int id);
        Task<bool> EnrollStudentAsync(int courseId, int studentId);
        Task<bool> UnenrollStudentAsync(int courseId, int studentId);
    }
}
