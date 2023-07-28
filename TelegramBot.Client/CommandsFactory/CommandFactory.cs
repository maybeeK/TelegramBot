using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Client.Commands;
using TelegramBot.Client.Commands.Interfaces;
using TelegramBot.Client.CommandsFactory.Abstract;
using TelegramBot.Client.Services.ServiceFactory.Abstact;

namespace TelegramBot.Client.CommandsFactory
{
    public class CommandFactory : CommadFactoryBase
    {
        private readonly IEnumerable<Type> _commands;
        
        public CommandFactory(TagServiceFactoryBase tagServiceFactoryBase, CourseServiceFactoryBase courseServiceFactoryBase) : base(courseServiceFactoryBase, tagServiceFactoryBase)
        {
            var type = typeof(ICommand);

            _commands = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(s => s.GetTypes())
                                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

        }
        public override ICommand Create(string stringCommand)
        {
            var command = _commands.FirstOrDefault(p => p.Name.ToLower().StartsWith(stringCommand.ToLower()));

            if (command == null)
            {
                command = typeof(NoExistingCommand);
            }

            object[] opts = new object[] {_tagServiceFactory.CreateTagService(), _courseServiceFactory.CreateCourseService() };

            return (ICommand)Activator.CreateInstance(command, args: opts);
        }
    }
}
