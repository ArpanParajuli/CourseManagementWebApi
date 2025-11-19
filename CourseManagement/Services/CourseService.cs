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
            var courses = await unitOfWork.courseRepository.GetAllCoursesAsync();

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
            var course = await unitOfWork.courseRepository.GetCourseByIdAsync(id);
            return course;
        }


        public async Task<bool> Create(Course course)
        {    
          var newCourse = await unitOfWork.courseRepository.AddCourseAsync(course);
          await unitOfWork.SaveAsync();
         return newCourse;
        }


         public async Task<bool> PutCourse(int id, Course course)
        {
      
            if (await unitOfWork.courseRepository.GetCourseByIdAsync(id) == null)
            {
               return false;
            }

            var success = await unitOfWork.courseRepository.UpdateCourseAsync(course);
            await unitOfWork.SaveAsync();

            return success;
        }


        public async Task<bool> Delete(int id)
        {
            var success = await unitOfWork.courseRepository.DeleteCourseAsync(id);
            await unitOfWork.SaveAsync();
            return success; 
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
