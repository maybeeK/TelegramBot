using Microsoft.EntityFrameworkCore;
using TelegramBot.API.Data;
using Microsoft.EntityFrameworkCore.InMemory;
using TelegramBot.API.Entities;
using TelegramBot.API.Services;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.Tests
{
    public class CourseServiceTests
    {
        private readonly AppDbContext _context;
        public CourseServiceTests()
        {
            var opts = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new AppDbContext(opts);
            _context.Courses.AddRange(GetTestCourses());
            _context.SaveChanges();
        }

        [Fact]
        public async void GetAllCoursesTest()
        {
            // Arrange
            var courses = Enumerable.Empty<Course>();

            // Act

            using (var context = _context)
            {
                var CS = new CourseService(context);
                courses = await CS.GetAllCourses();
            }

            // Assert
            Assert.Equal(10, courses.Count());
        }

        [Fact]
        public async void GetCoursesByTagTest()
        {
            // Arrange
            string tag = "name";
            var courses = Enumerable.Empty<Course>();

            // Act

            using (var context = _context)
            {
                var CS = new CourseService(context);
                courses = await CS.GetCoursesByTag(tag);
            }

            // Assert
            Assert.True(courses.All(e=>e.Name.ToLower().Contains(tag)));
        }

        [Fact]
        public async void GetCoursesByDiffLettersTagTest()
        {
            // Arrange
            var courses = Enumerable.Empty<Course>();
            string tag = "nAmE";

            // Act

            using (var context = _context)
            {
                var CS = new CourseService(context);
                courses = await CS.GetCoursesByTag(tag);
            }

            // Assert
            Assert.Equal(10, courses.Count());
        }

        [Fact]
        public async void GetOneCourseByTagTest()
        {
            // Arrange
            var courses = Enumerable.Empty<Course>();
            string tag = "#9";

            // Act

            using (var context = _context)
            {
                var CS = new CourseService(context);
                courses = await CS.GetCoursesByTag(tag);
            }

            // Assert
            Assert.Single(courses);
        }

        [Fact]
        public async void GetCourseTest()
        {
            // Arrange
            Course course;

            // Act

            using (var context = _context)
            {
                var CS = new CourseService(context);
                course = await CS.GetCourse(1);
            }

            // Assert
            Assert.Equal(1,course.Id);
        }

        [Fact]
        public async void GetNullCourseTest()
        {
            // Arrange
            Course course;

            // Act

            using (var context = _context)
            {
                var CS = new CourseService(context);
                course = await CS.GetCourse(100);
            }

            // Assert
            Assert.Null(course);
        }

        [Fact]
        public async void AddCourseTest()
        {
            // Arrange
            Course course;
            CourseDto courseDto = new CourseDto { Description= "New Course Descr", Name = "New Name", Link = "New Link" };

            // Act

            using (var context = _context)
            {
                var CS = new CourseService(context);
                course = await CS.AddCourse(courseDto);
            }

            // Assert
            Assert.Equal("New Name", course.Name);
        }

        [Fact]
        public async void DeleteCourseTest()
        {
            // Arrange
            Course course;
            int id = 1;

            // Act

            using (var context = _context)
            {
                var CS = new CourseService(context);
                course = await CS.DeleteCourse(id);
            }

            // Assert
            Assert.Equal(id, course.Id);
        }

        [Fact]
        public async void DeleteNotExistingCourseTest()
        {
            // Arrange
            Course course;
            int id = 100;

            // Act

            using (var context = _context)
            {
                var CS = new CourseService(context);
                course = await CS.DeleteCourse(id);
            }

            // Assert
            Assert.Null(course);
        }
        private IEnumerable<Course> GetTestCourses()
        {
            List<Course> courses = new List<Course>();
            for (int i = 1; i <= 10; i++)
            {
                courses.Add(new Course()
                {
                    Description = $"Descr #{i}",
                    Name = $"Name #{i}",
                    Link = $"Link #{i}"
                });
            }
            return courses;
        }
    }
}