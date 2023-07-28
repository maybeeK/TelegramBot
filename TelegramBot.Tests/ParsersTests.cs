using TelegramBot.API.CourseParsers;
using TelegramBot.API.Entities;

namespace TelegramBot.Tests
{
    public class ParsersTests
    {
        [Fact]
        public async void ITeaParserTest()
        {
            // Arrange
            List<Course> courses;
            ITeaParser Parser = new();

            // Act

            courses = (await Parser.Parse()).ToList();

            // Assert
            Assert.NotEmpty(courses);
        }
        [Fact]
        public async void MateAcademyParserTest()
        {
            // Arrange
            List<Course> courses;
            MateAcademyParser Parser = new();

            // Act

            courses = (await Parser.Parse()).ToList();

            // Assert
            Assert.NotEmpty(courses);
        }
        [Fact]
        public async void GoITParserTest()
        {
            // Arrange
            List<Course> courses;
            GoITParser Parser = new();

            // Act

            courses = (await Parser.Parse()).ToList();

            // Assert
            Assert.NotEmpty(courses);
        }
        [Fact]
        public async void BeetRootAcademyParserTest()
        {
            // Arrange
            List<Course> courses;
            BeetRootAcademyParser Parser = new();

            // Act

            courses = (await Parser.Parse()).ToList();

            // Assert
            Assert.NotEmpty(courses);
        }
    }
}
