using CourseManagement.DTOs;
using CourseManagement.Models;
using CourseManagement.Repositories;
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
        
         private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetStudentDto>>> GetStudents()
        {
            var query = _studentRepository.GetAllStudentsAsync();

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

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
        
        [HttpGet("{id}/courses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetStudentCourses(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            var courses = await _studentRepository.GetStudentCoursesAsync(id);
            return Ok(courses);
        }

  
        [HttpPost("create")]
        public async Task<ActionResult<Student>> Create([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var newStudent = await _studentRepository.AddStudentAsync(student);
            
            return CreatedAtAction(nameof(GetStudent), new { id = newStudent.Id }, newStudent);
        }


        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] Student student)
        {
            
            if (await _studentRepository.GetStudentByIdAsync(student.Id) == null)
            {
                return NotFound();
            }

            var success = await _studentRepository.UpdateStudentAsync(student);

            if (!success)
            {
                return StatusCode(500, "Failed to update student due to a database error.");
            }

            return NoContent(); 
        }

       
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _studentRepository.DeleteStudentAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
