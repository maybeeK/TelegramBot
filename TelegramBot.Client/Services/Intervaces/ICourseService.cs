using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.Client.Services.Intervaces
{
    public interface ICourseService : IDisposable
    {
        Task<IEnumerable<CourseDto>> GetCoursesByTag(string tag);
    }
}
