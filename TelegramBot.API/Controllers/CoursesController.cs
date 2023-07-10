using Microsoft.AspNetCore.Mvc;
using TelegramBot.API.Entities;
using TelegramBot.API.Services.Interfaces;

namespace TelegramBot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
        {
            try
            {
                var courses = await _courseService.GetAllCourses();

                if (courses == null)
                {
                    return NoContent();
                }

                return Ok(courses);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{tag}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByTag(string tag)
        {
            try
            {
                var courses = await _courseService.GetCoursesByTag(tag);

                if (courses == null)
                {
                    return NoContent();
                }

                return Ok(courses);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("Course/New")]
        public async Task<ActionResult<Course>> AddCourse([FromBody] Course courseToAdd)
        {
            try
            {
                var added = await _courseService.AddCourse(courseToAdd);

                if (added == null)
                {
                    return BadRequest();
                }

                return Ok(added);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("Course/{id:int}/Delete")]
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            try
            {
                var course = await _courseService.DeleteCourse(id);

                if (course == null)
                {
                    return NotFound();
                }

                return Ok(course);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
