using Microsoft.EntityFrameworkCore;
using TelegramBot.API.Data;
using TelegramBot.API.Entities;
using TelegramBot.API.Services.Interfaces;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.API.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;
        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Course> AddCourse(CourseDto newCourse)
        {
            var coursetoAdd = new Course()
            {
                Id = newCourse.Id,
                Name = newCourse.Name,
                Description = newCourse.Description,
                Link = newCourse.Link
            };

            var addedCourse = await _context.Courses.AddAsync(coursetoAdd);

            await _context.SaveChangesAsync();

            return addedCourse.Entity;
        }

        public async Task<Course> DeleteCourse(int id)
        {
            var courseToDelete = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (courseToDelete == null)
            {
                return default;
            }

            var deletedCourse = _context.Courses.Remove(courseToDelete);

            await _context.SaveChangesAsync();

            return deletedCourse.Entity;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course> GetCourse(int id)
        {
            return await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Course>> GetCoursesByTag(string tag)
        {
            return (await _context.Courses.ToListAsync()).Where(c=>c.Name.ToLower().Contains(tag.ToLower()));
        }
    }
}
