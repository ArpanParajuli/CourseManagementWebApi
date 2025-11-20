using CourseManagement.Repositories;
using CourseManagement.Data;
using CourseManagement.Models;

namespace CourseManagement.UnitOfWork
{
    public interface IUnitOfWork
    {
         IGenericRepository<Student> Students { get; }
         IGenericRepository<Course> Courses { get; }

         ICourseRepository courseRepository {get;}

         IStudentRepository studentRepository {get;}
        Task<int> SaveAsync();
    }
}
