using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Interfaces;

namespace TelegramBot.Client.Commands
{
    public class NoExistingCommand:ICommand
    {
        public Task<string> Process(string? body = null, long? userId = null, ParseMode? parseMode = null)
        {
            return Task.FromResult(
                $"There is no such command!\n" +
                $"/help - for all commands"
            );
        }
    }
}
