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
    public class MyTagsCommand : ICommand
    {
        public async Task<string> Process(string? body = null, long? userId = null, ParseMode? parseMode = null)
        {
            using (ITagService tagService = TagServiceFartory.GetTagService<TagService>())
            {
                var userTags = await tagService.GetTagsByUserId(userId.Value);

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
}
