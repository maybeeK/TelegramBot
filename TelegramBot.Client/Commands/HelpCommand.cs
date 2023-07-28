using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Abstact;
using TelegramBot.Client.Services.Intervaces;

namespace TelegramBot.Client.Commands
{
    public class HelpCommand : CommandBase
    {
        public HelpCommand(ITagService tagService, ICourseService courseService) : base(tagService, courseService) {
        
        }

        public override Task<string> Process(string? body, long? userId, ref ParseMode? parseMode)
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
