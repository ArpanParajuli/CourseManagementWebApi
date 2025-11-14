using CourseManagement.Data;
using CourseManagement.Models;
using Microsoft.EntityFrameworkCore;
using CourseManagement.Repositories;


namespace CourseManagement.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext CourseContext;
        public CourseRepository(AppDbContext CourseContext)
        {
            this.CourseContext = CourseContext;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await CourseContext.Courses
                                 .Include(c => c.Students)
                                 .ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await CourseContext.Courses
                                 .Include(c => c.Students)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> AddCourseAsync(Course course)
        {
            await CourseContext.Courses.AddAsync(course);
            await CourseContext.SaveChangesAsync();
            return course;
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            CourseContext.Courses.Update(course);
            return await CourseContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await CourseContext.Courses.FindAsync(id);
            if (course == null)
            {
                return false;
            }

            CourseContext.Courses.Remove(course);
            return await CourseContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> EnrollStudentAsync(int courseId, int studentId)
        {
            var course = await GetCourseByIdAsync(courseId);
            var student = await CourseContext.Students.FindAsync(studentId);

            if (course == null || student == null)
            {
                return false;
            }

            // Check if student is already enrolled xa kinai
            if (!course.Students.Any(s => s.Id == studentId))
            {
                course.Students.Add(student); // object of student will be saved as M | m is relationship 
                return await CourseContext.SaveChangesAsync() > 0;
            }
            
            return true; 
        }

        public async Task<bool> UnenrollStudentAsync(int courseId, int studentId)
        {
            var course = await GetCourseByIdAsync(courseId);
            var student = course?.Students.FirstOrDefault(s => s.Id == studentId);

            if (course == null || student == null)
            {
                return false;
            }

            course.Students.Remove(student); // remove both course id and student id in juntion table
            return await CourseContext.SaveChangesAsync() > 0;
        }



    }
}
