using Microsoft.AspNetCore.SignalR;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.API.Hubs
{
    public class CourseHub:Hub
    {
        public async Task NotifyAboutNewCourses(IEnumerable<CourseDto> courses)
        {
            await Clients.All.SendAsync("GetNewAddedCoursesOnClient", arg1: courses);
        }
    }
}
