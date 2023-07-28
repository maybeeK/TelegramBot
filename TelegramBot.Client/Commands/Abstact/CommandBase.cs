using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Interfaces;
using TelegramBot.Client.Services.Intervaces;

namespace TelegramBot.Client.Commands.Abstact
{
    public abstract class CommandBase : ICommand
    {
        protected readonly ITagService _tagService;
        protected readonly ICourseService _courseService;
        public CommandBase(ITagService tagService, ICourseService courseService)
        {
            _tagService = tagService;
            _courseService = courseService;
        }
        public abstract Task<string> Process(string? body, long? userId, ref ParseMode? parseMode);
    }
}
