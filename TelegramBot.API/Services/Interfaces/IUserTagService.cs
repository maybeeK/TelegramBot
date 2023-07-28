using TelegramBot.API.Entities;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.API.Services.Interfaces
{
    public interface IUserTagService
    {
        Task<IEnumerable<UserTag>> GetAllUserTags();
        Task<IEnumerable<UserTag>> GetTagsByUserId(long userId);
        Task<IEnumerable<UserTag>> AddUserTags(IEnumerable<UserTagDto> userTags);
        Task<IEnumerable<UserTag>> DeleteTagsByUserId(long userId);
    }
}
