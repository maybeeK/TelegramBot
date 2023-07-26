using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Client.Services.Intervaces;

namespace TelegramBot.Client.Services.ServiceFactory
{
    public static class TagServiceFartory
    {
        public static T GetTagService<T>() where T : ITagService
        {
            return (T)(object)new TagService(baseApiUrl: "https://localhost:7103/",
                                  maxTagsPerUser: 1);
        }
    }
}
