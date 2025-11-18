using CourseManagement.Models;
using CourseManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using CourseManagement.DTOs;
namespace CourseManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> Get()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();

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

            return Ok(courseDtos);
        }

     
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetById(int id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }


        [HttpPost("create")]
        public async Task<ActionResult<Course>> Create([FromBody] Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var newCourse = await _courseRepository.AddCourseAsync(course);


            return Ok("Course created successfully!");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

      
            if (await _courseRepository.GetCourseByIdAsync(id) == null)
            {
                return NotFound();
            }

            var success = await _courseRepository.UpdateCourseAsync(course);

            if (!success)
            {
                return StatusCode(500, "Failed to update course due to a database error.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _courseRepository.DeleteCourseAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); 
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudent([FromBody] EnrollDTO obj)
        {
            var success = await _courseRepository.EnrollStudentAsync(obj.CourseId, obj.StudentId);

            if (!success)
            {
                return NotFound("Course or Student not found.");
            }

            return Ok(new { message = $"Student {obj.StudentId} successfully enrolled in course {obj.CourseId}." });
        }


        [HttpPost("unenroll")]
        public async Task<IActionResult> UnenrollStudent([FromBody] UnEnrollDTO obj)
        {
            var success = await _courseRepository.UnenrollStudentAsync(obj.CourseId, obj.StudentId);

            if (!success)
            {
                return NotFound("Course or Student not found, or student was not enrolled.");
            }

            return Ok(new { message = $"Student {obj.CourseId} successfully unenrolled from course {obj.StudentId}." });
        }
    }
}