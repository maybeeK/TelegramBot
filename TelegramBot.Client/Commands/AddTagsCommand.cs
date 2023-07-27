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
using TelegramBot.Shared.DTOs;

namespace TelegramBot.Client.Commands
{
    public class AddTagsCommand : ICommand
    {
        public Task<string> Process(string? body, long? userId, ref ParseMode? parseMode)
        {
            var stringTags = body.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (stringTags.Length < 1)
            {
                return Task.FromResult($"Usage: /add <tags>");
            }

            return ProcessAsync(stringTags, userId.Value);
        }

        private async Task<string> ProcessAsync(string[] stringTags, long userId)
        {
            using (ITagService _tagService = TagServiceFartory.GetTagService<TagService>())
            {
                var userTags = stringTags.
                    Select(e => new UserTagDto()
                    {
                        UsertId = userId,
                        Tag = e
                    }
                );

                var added = await _tagService.AddUserTags(userId, userTags);

                if (added)
                {
                    return $"Tags {string.Join(' ' ,userTags.Select(e=>e.Tag))} added successfully!";
                }
                else
                {
                    return $"Can't add too much tags!";
                }
            }
        }

    }
}
