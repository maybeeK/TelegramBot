using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Abstact;
using TelegramBot.Client.Services.Intervaces;

namespace TelegramBot.Client.Commands
{
    public class ClearTagsCommand : CommandBase
    {
        public ClearTagsCommand(ITagService tagService, ICourseService courseService) : base(tagService, courseService)
        {

        }
        public override Task<string> Process(string? body, long? userId, ref ParseMode? parseMode)
        {
            return ProcessAsync(userId.Value);
        }
        private async Task<string> ProcessAsync(long userId)
        {
            var cleared = await _tagService.ClearUserTags(userId);

            if (cleared)
            {
                return "Tags cleared!";
            }
            else
            {
                return "Can't cleat tags(((";
            }
        }
    }
}
