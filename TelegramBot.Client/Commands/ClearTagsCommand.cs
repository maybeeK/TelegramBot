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
    public class ClearTagsCommand : ICommand
    {
        public async Task<string> Process(string? body = null, long? userId = null, ParseMode? parseMode = null)
        {
            using (ITagService _tagService = new TagService())
            {
                var cleared = await _tagService.ClearUserTags(userId.Value);

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
