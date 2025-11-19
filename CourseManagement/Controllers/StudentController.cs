using CourseManagement.DTOs;
using CourseManagement.Models;
using CourseManagement.Repositories;
using CourseManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CourseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        
        private readonly IStudentService studentService;
        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetStudentDto>>> GetStudents()
        {

 
            var allstudents = await studentService.GetStudents();

            return Ok(allstudents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await studentService.GetStudent(id);
            

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
        
        [HttpGet("{id}/courses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetStudentCourses(int id)
        {
            // var student = await _studentRepository.GetStudentByIdAsync(id);
            // var student = 

            // if (student == null)
            // {
            //     return NotFound($"Student with ID {id} not found.");
            // }

            var studentcourses = await studentService.GetStudentCourses(id);
            return Ok(studentcourses);
        }

  
        [HttpPost("create")]
        public async Task<ActionResult<Student>> Create([FromBody] Student student)
        {
            // var newStudent = await _studentRepository.AddStudentAsync(student);
            var newStudent = await studentService.Create(student);
            return CreatedAtAction(nameof(GetStudent), new { id = newStudent.Id }, newStudent);
        }


        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] Student student)
        {
            
            // if (await _studentRepository.GetStudentByIdAsync(student.Id) == null)
            // {
            //     return NotFound();
            // }

            // var success = await _studentRepository.UpdateStudentAsync(student);
            var success = await studentService.Edit(student);


            if (!success)
            {
                return StatusCode(500, "Failed to update student due to a database error.");
            }

            return NoContent(); 
        }

       
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // var success = await _studentRepository.DeleteStudentAsync(id);
            var success = await studentService.Delete(id);


            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
