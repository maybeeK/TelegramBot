using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Interfaces;
using TelegramBot.Client.Services;
using TelegramBot.Client.Services.Intervaces;

namespace TelegramBot.Client.Commands
{
    public class CheckCommand : ICommand
    {
        public async Task<string> Process(string? body = null, long? userId = null, ParseMode? parseMode = null)
        {
            if (!IsBodyValid(body))
            {
                return $"Usage: /check <tag>";
            }
            else
            {
                using (ICourseService courseService = new CourseService())
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
        }

        private bool IsBodyValid(string body)
        {
            return !string.IsNullOrEmpty(body) && body.Split(" ").Length < 2;
        }
    }
}
