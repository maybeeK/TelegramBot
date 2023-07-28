using TelegramBot.Shared.DTOs;

namespace TelegramBot.Client.Services.Interfaces
{
    public interface INewCourseNotifier
    {
        Task Notify(IEnumerable<CourseDto> newCourses);
    }
}
