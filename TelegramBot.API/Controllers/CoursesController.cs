using Microsoft.AspNetCore.Mvc;
using TelegramBot.API.Entities;
using TelegramBot.API.Extensions;
using TelegramBot.API.Services.Interfaces;
using TelegramBot.Shared.DTOs;

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
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses()
        {
            try
            {
                var courses = await _courseService.GetAllCourses();

                if (courses == null)
                {
                    return NoContent();
                }

                var coursesDto = courses.ConvertToDto(); 

                return Ok(coursesDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{tag}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByTag(string tag)
        {
            try
            {
                var courses = await _courseService.GetCoursesByTag(tag);

                if (courses == null)
                {
                    return NoContent();
                }

                var coursesDto = courses.ConvertToDto();

                return Ok(coursesDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("Course/New")]
        public async Task<ActionResult<CourseDto>> AddCourse([FromBody] CourseDto courseToAdd)
        {
            try
            {
                var added = await _courseService.AddCourse(courseToAdd);

                if (added == null)
                {
                    return BadRequest();
                }

                var addedDto = added.ConvertToDto();

                return Ok(addedDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("Course/{id:int}/Delete")]
        public async Task<ActionResult<CourseDto>> DeleteCourse(int id)
        {
            try
            {
                var course = await _courseService.DeleteCourse(id);

                if (course == null)
                {
                    return NotFound();
                }

                var courseDto = course.ConvertToDto();

                return Ok(courseDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
