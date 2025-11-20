using CourseManagement.Models;
using CourseManagement.DTOs;
using CourseManagement.UnitOfWork;
namespace CourseManagement.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;


        public CourseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<CourseDto?>> GetAllCourses()
        {
            var courses =  unitOfWork.Courses.GetAllAsync();

            var courseDtos = courses.Select(course => new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Students = course.Students.Select(student => new GetStudentDto
                {
                    Id = student.Id,
                    FullName = student.FullName
                }).ToList()
            }).ToList();

            return courseDtos;
        }


        public async Task<Course?> GetById(int id)
        {
            var course = await unitOfWork.Courses.GetByIdAsync(id);
            return course;
        }


        public async Task<bool> Create(Course course)
        {    
          await unitOfWork.Courses.AddAsync(course);
          await unitOfWork.SaveAsync();
         return true;
        }


         public async Task<bool> PutCourse(int id, Course course)
        {
      
            if (await unitOfWork.Courses.GetByIdAsync(id) == null)
            {
               return false;
            }

             unitOfWork.Courses.Update(course);
            await unitOfWork.SaveAsync();

            return true;
        }


        public async Task<bool> Delete(int id)
        {
            var getcourseobj = await unitOfWork.Courses.GetByIdAsync(id);
            unitOfWork.Courses.Delete(getcourseobj);
            await unitOfWork.SaveAsync();
            return true; 
        }


        public async Task<bool> EnrollStudent(EnrollDTO obj)
        {
            var success = await unitOfWork.courseRepository.EnrollStudentAsync(obj.CourseId, obj.StudentId);
            await unitOfWork.SaveAsync();
            return success;
        }
        public async Task<bool> UnenrollStudent(UnEnrollDTO obj)
        {
            var success = await unitOfWork.courseRepository.UnenrollStudentAsync(obj.CourseId, obj.StudentId);
            await unitOfWork.SaveAsync();
            return success;
        }


    }
}
