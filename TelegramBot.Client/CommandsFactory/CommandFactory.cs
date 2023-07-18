using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Client.Commands;
using TelegramBot.Client.Commands.Interfaces;

namespace TelegramBot.Client.CommandsFactory
{
    public class CommandFactory
    {
        private readonly IEnumerable<Type> _commands;
        public CommandFactory()
        {
            var type = typeof(ICommand);

            _commands = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(s => s.GetTypes())
                                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);
        }
        public ICommand Create(string stringCommand)
        {
            var command = _commands.FirstOrDefault(p => p.Name.ToLower().StartsWith(stringCommand.ToLower()));

            if (command == null)
            {
                command = typeof(NoExistingCommand);
            }

            return (ICommand)Activator.CreateInstance(command);
        }
    }
}
