using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Client.Services.Intervaces;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.Client.Services
{
    public class TagService : ITagService
    {
        private readonly int MAX_TAGS_PER_USER; /*= 2;*/

        private readonly HttpClient _httpClient;
        public TagService(string baseApiUrl, int maxTagsPerUser)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseApiUrl)
            };

            MAX_TAGS_PER_USER = maxTagsPerUser;
        }
        public async Task<bool> AddUserTags(long userId, IEnumerable<UserTagDto> userTags)
        {
            try
            {
                int userTagsAmount = (await GetTagsByUserId(userId)).Count();
                if (userTagsAmount + userTags.Count() > MAX_TAGS_PER_USER)
                {
                    return false;
                }

                var responce = await _httpClient.PostAsJsonAsync($"api/UserTags", userTags);

                if (!responce.IsSuccessStatusCode)
                {
                    return false;
                }

                return true;

            }
            catch (Exception)
            {
                Console.WriteLine("SERVICE ERROR!!!");
                throw;
            }
        }
        public async Task<IEnumerable<UserTagDto>> GetTagsByUserId(long userId)
        {
            try
            {
                var responce = await _httpClient.GetAsync($"api/UserTags/{userId}");

                if (responce.IsSuccessStatusCode)
                {
                    if (responce.StatusCode == HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<UserTagDto>();
                    }

                    return await responce.Content.ReadFromJsonAsync<IEnumerable<UserTagDto>>();
                }
                else 
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> ClearUserTags(long userId)
        {
            try
            {
                var responce = await _httpClient.DeleteAsync($"api/UserTags/{userId}");

                if (responce.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("SERVICE ERROR!!!");
                throw;
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public async Task<IEnumerable<UserTagDto>> GetAllUserTags()
        {
            try
            {
                var responce = await _httpClient.GetAsync("api/UserTags");

                if (responce.IsSuccessStatusCode)
                {
                    return await responce.Content.ReadFromJsonAsync<IEnumerable<UserTagDto>>();
                }
                
                return Enumerable.Empty<UserTagDto>();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
