using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Client.Commands.Interfaces
{
    public interface ICommand
    {
        Task<string> Process(string? body = null,long? userId = null ,ParseMode? parseMode = null);
    }
}
