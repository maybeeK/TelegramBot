using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Client.Services.Intervaces;

namespace TelegramBot.Client.Services.ServiceFactory
{
    public static class CourseServiceFactory
    {
        public static T GetCourseService<T>() where T : ICourseService
        {
            return (T)(object)new CourseService(baseApiUrl: "https://localhost:7103/");
        }
    }
}
