using TelegramBot.Shared.DTOs;

namespace TelegramBot.Client.Services.Intervaces
{
    public interface ICourseService : IDisposable
    {
        Task<IEnumerable<CourseDto>> GetCoursesByTag(string tag);
    }
}
