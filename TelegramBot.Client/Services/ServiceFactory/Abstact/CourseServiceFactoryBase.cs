using TelegramBot.Client.Services.Intervaces;

namespace TelegramBot.Client.Services.ServiceFactory.Abstact
{
    public abstract class CourseServiceFactoryBase
    {
        public abstract ICourseService CreateCourseService();
    }
}
