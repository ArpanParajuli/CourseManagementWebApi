using CourseManagement.Models;
using CourseManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using CourseManagement.DTOs;
using CourseManagement.Services;
namespace CourseManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
     

        private readonly ICourseService courseService;



        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto?>>> Get()
        {
            var courseDtos = await courseService.GetAllCourses();
            return Ok(courseDtos);
        }

     
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto?>> GetById(int id)
        {
            // var course = await _courseRepository.GetCourseByIdAsync(id);
            var course = await courseService.GetById(id);

            if (course == null)
            {
                return NotFound();
            }

            var sendcourse = new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
            };

            return Ok(course);
        }


        [HttpPost("create")]
        public async Task<ActionResult<Course>> Create([FromBody] Course course)
        {

            
            // var newCourse = await _courseRepository.AddCourseAsync(course);
            var newCourse = await courseService.Create(course);



            return Ok("Course created successfully!");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }
 
            // var success = await _courseRepository.UpdateCourseAsync(course);

            var success = await courseService.PutCourse(id , course);


            if (!success)
            {
                return StatusCode(500, "Failed to update course due to a database error.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // var success = await _courseRepository.DeleteCourseAsync(id);
            var success = await courseService.Delete(id);


            if (!success)
            {
                return NotFound();
            }

            return NoContent(); 
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudent([FromBody] EnrollDTO obj)
        {
            // var success = await _courseRepository.EnrollStudentAsync(obj.CourseId, obj.StudentId);
            var success = await courseService.EnrollStudent(obj);


            if (!success)
            {
                return NotFound("Course or Student not found.");
            }

            return Ok(new { message = $"Student {obj.StudentId} successfully enrolled in course {obj.CourseId}." });
        }


        [HttpPost("unenroll")]
        public async Task<IActionResult> UnenrollStudent([FromBody] UnEnrollDTO obj)
        {
            // var success = await _courseRepository.UnenrollStudentAsync(obj.CourseId  , obj.StudentId);
            var success = await courseService.UnenrollStudent(obj);

            if (!success)
            {
                return NotFound("Course or Student not found, or student was not enrolled.");
            }

            return Ok(new { message = $"Student {obj.CourseId} successfully unenrolled from course {obj.StudentId}." });
        }
    }
}