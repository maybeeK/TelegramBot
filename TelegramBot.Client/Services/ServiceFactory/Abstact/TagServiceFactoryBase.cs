using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Client.Services.Intervaces;

namespace TelegramBot.Client.Services.ServiceFactory.Abstact
{
    public abstract class TagServiceFactoryBase
    {
        public abstract ITagService CreateTagService();
    }
}
