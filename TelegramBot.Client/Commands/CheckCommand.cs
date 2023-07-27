using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Interfaces;
using TelegramBot.Client.Services;
using TelegramBot.Client.Services.Intervaces;
using TelegramBot.Client.Services.ServiceFactory;

namespace TelegramBot.Client.Commands
{
    public class CheckCommand : ICommand
    {
        public Task<string> Process(string? body, long? userId, ref ParseMode? parseMode)
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
            using (ICourseService courseService = CourseServiceFactory.GetCourseService<CourseService>())
            {
                StringBuilder sb = new StringBuilder();
                var courses = (await courseService.GetCoursesByTag(body)).ToList();

                sb.AppendLine($"Founded {courses.Count()} courses");
                courses.ForEach((c) =>
                {
                    sb.AppendLine($"[{c.Name}]({c.Link}) - {c.Description}");
                });

                return sb.ToString();
            }
        }
        private bool IsBodyValid(string body)
        {
            return !string.IsNullOrEmpty(body) && body.Split(" ").Length < 2;
        }
    }
}
