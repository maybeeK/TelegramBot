using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.Client.Services.Intervaces
{
    public interface ITagService : IDisposable
    {
        Task<bool> ClearUserTags(long userId);
        Task<bool> AddUserTags(long userId, IEnumerable<UserTagDto> userTags);
    }
}
