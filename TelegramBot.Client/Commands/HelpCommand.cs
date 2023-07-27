using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Interfaces;

namespace TelegramBot.Client.Commands
{
    public class HelpCommand : ICommand
    {
        public Task<string> Process(string? body, long? userId, ref ParseMode? parseMode)
        {
            return Task.FromResult(
                $"/start - to start\n" +
                $"/help - all commands and usage\n" +
                $"/check <tag> - check existing courses by tag\n" +
                $"/add <tags> - add tags for notification\n" +
                $"/clear - delete all tags\n" +
                $"/mytags - check your tags"
            );
        }
    }
}
