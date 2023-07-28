using TelegramBot.Client.Services.Intervaces;
using TelegramBot.Client.Services.ServiceFactory.Abstact;

namespace TelegramBot.Client.Services.ServiceFactory
{
    public class TagServiceFartory : TagServiceFactoryBase
    {
        private readonly string _baseApiUrl;
        private readonly int _tagsPerUser;
        public TagServiceFartory(string baseApiUrl, int tagsPerUser) { 
            _baseApiUrl = baseApiUrl;
            _tagsPerUser = tagsPerUser;
        }
        public override ITagService CreateTagService()
        {
            return new TagService(baseApiUrl: _baseApiUrl, maxTagsPerUser: _tagsPerUser);
        }
    }
}
