using TelegramBot.API.Entities;

namespace TelegramBot.API.CourseParsers.Interfaces
{
    public interface ICourseParser
    {
        Task<IEnumerable<Course>> Parse();
    }
}
