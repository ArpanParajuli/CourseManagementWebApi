using CourseManagement.Models;
using CourseManagement.Data;
using Microsoft.EntityFrameworkCore;
namespace CourseManagement.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext StudentContext;

        public StudentRepository(AppDbContext StudentContext)
        {
            this.StudentContext = StudentContext;
        }


        public IQueryable<Student> GetAllStudentsAsync()
        {
            return StudentContext.Students
                         .Include(s => s.Courses);

        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await StudentContext.Students
                                 .Include(s => s.Courses)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            await StudentContext.Students.AddAsync(student);
            // await StudentContext.SaveChangesAsync();
            return student;
        }

    public async Task<bool> UpdateStudentAsync(Student student)
{
    var studentobj = await StudentContext.Students.FirstOrDefaultAsync(s => s.Id == student.Id);
    if (studentobj == null)
        return false;

    studentobj.Email = student.Email;
    studentobj.FullName = student.FullName;

    // return await StudentContext.SaveChangesAsync() > 0;
    return true;
}
 

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await StudentContext.Students.FindAsync(id);
            if (student == null)
            {
                return false;
            }

            StudentContext.Students.Remove(student);
            // await StudentContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Course>> GetStudentCoursesAsync(int studentId)
        {
            var student = await StudentContext.Students
                                        .Include(s => s.Courses)
                                        .FirstOrDefaultAsync(s => s.Id == studentId);
                                        
            return student?.Courses ?? Enumerable.Empty<Course>();
        }
    }
}
