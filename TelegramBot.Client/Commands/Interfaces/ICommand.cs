using Telegram.Bot.Types.Enums;

namespace TelegramBot.Client.Commands.Interfaces
{
    public interface ICommand
    {
        Task<string> Process(string? body,long? userId , ref ParseMode? parseMode);
    }
}
