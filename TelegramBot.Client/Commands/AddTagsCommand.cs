using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Interfaces;
using TelegramBot.Client.Services;
using TelegramBot.Client.Services.Intervaces;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.Client.Commands
{
    public class AddTagsCommand : ICommand
    {
        public async Task<string> Process(string? body = null, long? userId = null, ParseMode? parseMode = null)
        {
            var stringTags = body.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (stringTags.Length < 1)
            {
                return $"Usage: /add <tags>";
            }

            using (ITagService _tagService = new TagService())
            {
                var userTags = stringTags.
                    Select(e=>new UserTagDto()
                        { 
                            UsertId = userId.Value,
                            Tag = e}
                        );

                var added = await _tagService.AddUserTags(userId.Value, userTags);

                if (added)
                {
                    return $"Tags added successfully!";
                }
                else
                {
                    return $"Can't add too much tags!";
                }
            }
        }
    }
}
