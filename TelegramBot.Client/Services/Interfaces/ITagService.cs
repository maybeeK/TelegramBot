using TelegramBot.Shared.DTOs;

namespace TelegramBot.Client.Services.Intervaces
{
    public interface ITagService : IDisposable
    {
        Task<IEnumerable<UserTagDto>> GetAllUserTags();
        Task<IEnumerable<UserTagDto>> GetTagsByUserId(long userId);
        Task<bool> ClearUserTags(long userId);
        Task<bool> AddUserTags(long userId, IEnumerable<UserTagDto> userTags);
    }
}
