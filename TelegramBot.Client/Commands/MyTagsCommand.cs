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
    public class MyTagsCommand : ICommand
    {
        public Task<string> Process(string? body, long? userId, ref ParseMode? parseMode)
        {
            return ProcessAsync(userId.Value);
        }
        private async Task<string> ProcessAsync(long userId)
        {
            using (ITagService tagService = TagServiceFartory.GetTagService<TagService>())
            {
                var userTags = await tagService.GetTagsByUserId(userId);

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
