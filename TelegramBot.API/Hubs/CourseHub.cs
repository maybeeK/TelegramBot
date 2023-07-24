using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
