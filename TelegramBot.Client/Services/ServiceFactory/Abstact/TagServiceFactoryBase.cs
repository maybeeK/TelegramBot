using TelegramBot.Client.Services.Intervaces;

namespace TelegramBot.Client.Services.ServiceFactory.Abstact
{
    public abstract class TagServiceFactoryBase
    {
        public abstract ITagService CreateTagService();
    }
}
