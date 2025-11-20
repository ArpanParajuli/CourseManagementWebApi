
using CourseManagement.DTOs;
using CourseManagement.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseManagement.Models;
using CourseManagement.Repositories;
namespace CourseManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IStudentRepository studentRepository;

        private readonly ICourseRepository courseRepository;
        public StudentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetStudentDto>> GetStudents()
        {
            var query = unitOfWork.Students.GetAllAsync();

            var students =  query
                .Select(s => new GetStudentDto
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Email = s.Email,
                    Courses = s.Courses.Select(c => new CourseDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description

                    }).ToList()
                })
                .ToList();

            return students;
        }

        public async Task<GetStudentDto?> GetStudent(int id)
        {
            var student = await unitOfWork.Students.GetByIdAsync(id);

            var userdata = new GetStudentDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email
            };
            
            return userdata;
        }


        public async Task<IEnumerable<Course?>> GetStudentCourses(int id)
        {
            var student = await unitOfWork.Students.GetByIdAsync(id);

          if (student == null)
          {
             return Enumerable.Empty<Course>();
          }
            var courses = await unitOfWork.studentRepository.GetStudentCoursesAsync(id);
            return courses;
        }

         public async Task<Student> Create(Student student)
        {
            await unitOfWork.Students.AddAsync(student);   
            await unitOfWork.SaveAsync();      
            return student;
        }


        public async Task<bool> Edit(Student student)
        {
            
            var isStudentValid = await unitOfWork.Students.GetByIdAsync(student.Id);
            
            if(isStudentValid == null)
            {
                return false;
            }

             unitOfWork.Students.Update(student);

             await unitOfWork.SaveAsync();

             return true;
        }


        public async Task<bool> Delete(int id)
        {
            var getstudentobj = await unitOfWork.Students.GetByIdAsync(id);
             unitOfWork.Students.Delete(getstudentobj);
            await unitOfWork.SaveAsync();
            return true;
        }



    }
}
