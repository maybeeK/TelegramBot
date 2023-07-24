using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.Client.Services.Interfaces
{
    public interface INewCourseNotifier
    {
        Task Notify(IEnumerable<CourseDto> newCourses);
    }
}
