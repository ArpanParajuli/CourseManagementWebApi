using CourseManagement.Repositories;
using CourseManagement.Data;
using CourseManagement.Models;

namespace CourseManagement.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
      private readonly AppDbContext _context;
    public IGenericRepository<Student> Students { get; }
    public IGenericRepository<Course> Courses { get; }

    public ICourseRepository   courseRepository {get;}

    public IStudentRepository studentRepository {get;}

    public UnitOfWork(AppDbContext context ,
                     IGenericRepository<Student> Students , 
                     IGenericRepository<Course> Courses,
                     ICourseRepository courseRepository,
                     IStudentRepository studentRepository
                     )
    {
        _context = context;
        // Students = new GenericRepository<Student>(_context);
        // Courses = new GenericRepository<Course>(_context);

        this.Students = Students;
        this.Courses = Courses;

        this.courseRepository = courseRepository;
        this.studentRepository = studentRepository;
        
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    }
}
