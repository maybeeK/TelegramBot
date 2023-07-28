using System.Text;
using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Abstact;
using TelegramBot.Client.Services.Intervaces;

namespace TelegramBot.Client.Commands
{
    public class CheckCommand : CommandBase
    {
        public CheckCommand(ITagService tagService, ICourseService courseService) : base(tagService, courseService)
        {

        }
        public override Task<string> Process(string? body, long? userId, ref ParseMode? parseMode)
        {
            if (!IsBodyValid(body))
            {
                return Task.FromResult($"Usage: /check <tag>");
            }
            else
            {
                parseMode = ParseMode.Markdown;
                return ProcessAsync(body);
            }
        }
        private async Task<string> ProcessAsync(string body)
        {

            StringBuilder sb = new StringBuilder();
            var courses = (await _courseService.GetCoursesByTag(body)).ToList();

            sb.AppendLine($"Founded {courses.Count()} courses");
            courses.ForEach((c) =>
            {
                sb.AppendLine($"[{c.Name}]({c.Link}) - {c.Description}");
            });

            return sb.ToString();

        }
        private bool IsBodyValid(string body)
        {
            return !string.IsNullOrEmpty(body) && body.Split(" ").Length < 2;
        }
    }
}
