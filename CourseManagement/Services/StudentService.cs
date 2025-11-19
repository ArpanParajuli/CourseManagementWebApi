
using CourseManagement.DTOs;
using CourseManagement.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseManagement.Models;
namespace CourseManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork unitOfWork;
        public StudentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetStudentDto>> GetStudents()
        {
            var query = unitOfWork.studentRepository.GetAllStudentsAsync();

            var students = await query
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
                .ToListAsync();

            return students;
        }

        public async Task<GetStudentDto?> GetStudent(int id)
        {
            var student = await unitOfWork.studentRepository.GetStudentByIdAsync(id);

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
            var student = await unitOfWork.studentRepository.GetStudentByIdAsync(id);

          if (student == null)
          {
             return Enumerable.Empty<Course>();
          }
            var courses = await unitOfWork.studentRepository.GetStudentCoursesAsync(id);
            return courses;
        }

         public async Task<Student> Create(Student student)
        {
            var newStudent = await unitOfWork.studentRepository.AddStudentAsync(student);   
            await unitOfWork.SaveAsync();      
            return newStudent;
        }


        public async Task<bool> Edit(Student student)
        {
            
            var isStudentValid = await unitOfWork.studentRepository.GetStudentByIdAsync(student.Id);
            
            if(isStudentValid == null)
            {
                return false;
            }

             var success = await unitOfWork.studentRepository.UpdateStudentAsync(student);

             await unitOfWork.SaveAsync();

             return success;
        }


        public async Task<bool> Delete(int id)
        {
            var success = await unitOfWork.studentRepository.DeleteStudentAsync(id);
            await unitOfWork.SaveAsync();
            return success;
        }



    }
}
