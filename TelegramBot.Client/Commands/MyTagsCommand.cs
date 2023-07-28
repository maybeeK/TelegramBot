using System.Text;
using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Abstact;
using TelegramBot.Client.Services.Intervaces;

namespace TelegramBot.Client.Commands
{
    public class MyTagsCommand : CommandBase
    {
        public MyTagsCommand(ITagService tagService, ICourseService courseService) : base(tagService, courseService)
        {

        }
        public override Task<string> Process(string? body, long? userId, ref ParseMode? parseMode)
        {
            return ProcessAsync(userId.Value);
        }
        private async Task<string> ProcessAsync(long userId)
        {
            var userTags = await _tagService.GetTagsByUserId(userId);

            if (userTags.Any())
            {
                StringBuilder sb = new StringBuilder("Your tags:\n");
                foreach (var item in userTags)
                {
                    sb.AppendLine(item.Tag);
                }
                return sb.ToString();
            }
            else
            {
                return "You have no tags(";
            }
        }
    }
}
