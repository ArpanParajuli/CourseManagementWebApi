using CourseManagement.Repositories;
using CourseManagement.DTOs;
using Microsoft.AspNetCore.Mvc;
using CourseManagement.Models;

namespace CourseManagement.Services
{
    public interface IStudentService
    {
         Task<IEnumerable<GetStudentDto>> GetStudents();

         Task<GetStudentDto?> GetStudent(int id);

         Task<IEnumerable<Course?>> GetStudentCourses(int id);

         Task<Student> Create(Student student);


         Task<bool> Edit(Student student);

         Task<bool> Delete(int id);
    }
}
