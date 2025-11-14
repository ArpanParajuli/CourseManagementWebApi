using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CourseManagement.Repositories;
using CourseManagement.Models;

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
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
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
