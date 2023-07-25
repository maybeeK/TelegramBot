using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Services.Interfaces;
using TelegramBot.Client.Services.Intervaces;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.Client.Services
{
    public class NewCourseNotifier : INewCourseNotifier
    {
        private readonly TelegramBotClient _bot;
        private readonly ITagService _tagService;
        public NewCourseNotifier(TelegramBotClient bot, ITagService tagService)
        {
            _bot = bot;
            _tagService = tagService;
        }

        public async Task Notify(IEnumerable<CourseDto> newCourses)
        {
            var tagsGroup = (await _tagService.GetAllUserTags())
                                .Where(e=>newCourses.Any(nc=>nc.Name.ToLower().Contains(e.Tag.ToLower())))
                                .GroupBy(e=>e.UsertId);

            foreach (var tags in tagsGroup)
            {
                await NotifyUser(newCourses, tags);
            }
        }

        private async Task NotifyUser(IEnumerable<CourseDto> newCourses, IGrouping<long, UserTagDto> userTags)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Foud new course for you!!");
            foreach (var tag in userTags)
            {
                newCourses.ToList().ForEach(nc =>
                {
                    if (nc.Name.ToLower().Contains(tag.Tag.ToLower()))
                    {
                        sb.AppendLine($"[{nc.Name}]({nc.Link}) - {nc.Description}");
                    }
                });
            }
            await _bot.SendTextMessageAsync(chatId: userTags.Key, text: sb.ToString(), disableWebPagePreview: true, parseMode: ParseMode.Markdown);
        }
    }
}
