using CourseManagement.Repositories;
using CourseManagement.Data;

namespace CourseManagement.UnitOfWork
{
    public interface IUnitOfWork
    {
        IStudentRepository studentRepository { get; }
        ICourseRepository courseRepository {get;}
        Task<int> SaveAsync();
    }
}
