using CourseManagement.DTOs;
using CourseManagement.Models;

namespace CourseManagement.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto?>> GetAllCourses();
        Task<Course?> GetById(int id);
        Task<bool> Create(Course course);
        Task<bool> PutCourse(int id, Course course);
        Task<bool> Delete(int id);
        Task<bool> EnrollStudent(EnrollDTO obj);
        Task<bool> UnenrollStudent(UnEnrollDTO obj);
    }
}
