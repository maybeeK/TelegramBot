using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Abstact;
using TelegramBot.Client.Services.Intervaces;

namespace TelegramBot.Client.Commands
{
    public class NoExistingCommand:CommandBase
    {
        public NoExistingCommand(ITagService tagService, ICourseService courseService):base(tagService, courseService)
        {

        }
        public override Task<string> Process(string? body, long? userId, ref ParseMode? parseMode)
        {
            return Task.FromResult(
                $"There is no such command!\n" +
                $"/help - for all commands"
            );
        }
    }
}
