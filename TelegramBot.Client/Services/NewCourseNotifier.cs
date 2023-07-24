using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Client.Services.Interfaces;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.Client.Services
{
    public class NewCourseNotifier : INewCourseNotifier
    {
        public Task Notify(IEnumerable<CourseDto> newCourses) //TODO Implement interface
        {
            throw new NotImplementedException();
        }
    }
}
