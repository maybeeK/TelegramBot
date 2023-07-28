using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Abstact;
using TelegramBot.Client.Services.Intervaces;

namespace TelegramBot.Client.Commands
{
    public class StartCommand:CommandBase
    {
        public StartCommand(ITagService tagService, ICourseService courseService):base(tagService, courseService)
        {

        }
        public override Task<string> Process(string? body, long? userId, ref ParseMode? parseMode)
        {
            return Task.FromResult($"Hello! I am Bot!\n/help - for more commands!");
        }
    }
}
