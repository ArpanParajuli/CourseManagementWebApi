using CourseManagement.Repositories;
using CourseManagement.Data;

namespace CourseManagement.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;

        public IStudentRepository studentRepository {get;}
        public ICourseRepository courseRepository {get;}
        public UnitOfWork(AppDbContext appDbContext , IStudentRepository studentRepository , ICourseRepository courseRepository)
        {
            this.appDbContext = appDbContext;
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
        }

        // public IStudentRepository studentRepository {get;}
        // public ICourseRepository courseRepository {get;}
        // public UnitOfWork(AppDbContext appDbContext)
        // {
        //     this.appDbContext = appDbContext;
        //     studentRepository = new StudentRepository(appDbContext);
        //     courseRepository = new CourseRepository(appDbContext);
        // }



        public async Task<int> SaveAsync()
        {
            return await appDbContext.SaveChangesAsync();
        }
    }
}
