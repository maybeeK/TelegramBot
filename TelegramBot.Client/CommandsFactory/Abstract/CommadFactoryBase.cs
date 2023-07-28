using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Client.Commands.Interfaces;
using TelegramBot.Client.Services.ServiceFactory.Abstact;

namespace TelegramBot.Client.CommandsFactory.Abstract
{
    public abstract class CommadFactoryBase
    {
        protected readonly CourseServiceFactoryBase _courseServiceFactory;
        protected readonly TagServiceFactoryBase _tagServiceFactory;
        public CommadFactoryBase(CourseServiceFactoryBase courseServiceFactory, TagServiceFactoryBase tagServiceFactory)
        {
            _courseServiceFactory = courseServiceFactory;
            _tagServiceFactory = tagServiceFactory;
        }
        public abstract ICommand Create(string stringCommand);
    }
}
