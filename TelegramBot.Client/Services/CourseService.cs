using System.Net;
using System.Net.Http.Json;
using TelegramBot.Client.Services.Intervaces;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.Client.Services
{
    public class CourseService : ICourseService
    {
        private readonly HttpClient _httpClient;
        public CourseService(string baseApiUrl)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseApiUrl)
            };
        }
        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByTag(string tag)
        {
            try
            {
                var responce = await _httpClient.GetAsync($"api/Courses/{tag}");

                if (responce.IsSuccessStatusCode)
                {

                    if (responce.StatusCode == HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CourseDto>();
                    }

                    return await responce.Content.ReadFromJsonAsync<IEnumerable<CourseDto>>();

                }
                else
                {
                    var message = await responce.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {responce.StatusCode}\nMessage: {message}");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("SERVICE ERROR!!!");
                throw;
            }
        }
    }
}
