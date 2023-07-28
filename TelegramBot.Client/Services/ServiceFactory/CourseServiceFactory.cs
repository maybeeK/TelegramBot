using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Client.Services.Intervaces;
using TelegramBot.Client.Services.ServiceFactory.Abstact;

namespace TelegramBot.Client.Services.ServiceFactory
{
    public class CourseServiceFactory : CourseServiceFactoryBase
    {
        private readonly string _baseApiUrl;
        public CourseServiceFactory(string baseApiUrl)
        {
            _baseApiUrl= baseApiUrl;
        }
        public override ICourseService CreateCourseService()
        {
            return new CourseService(baseApiUrl: _baseApiUrl);
        }
    }
}
