using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Interfaces;
using TelegramBot.Client.Services;
using TelegramBot.Client.Services.Intervaces;
using TelegramBot.Client.Services.ServiceFactory;

namespace TelegramBot.Client.Commands
{
    public class ClearTagsCommand : ICommand
    {
        public Task<string> Process(string? body, long? userId, ref ParseMode? parseMode)
        {
            return ProcessAsync(userId.Value);
        }
        private async Task<string> ProcessAsync(long userId)
        {
            using (ITagService _tagService = TagServiceFartory.GetTagService<TagService>())
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
}
