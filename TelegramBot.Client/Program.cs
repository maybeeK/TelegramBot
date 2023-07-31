using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using TelegramBot.Client.Bot;
using TelegramBot.Client.CommandsFactory;
using TelegramBot.Client.Services;
using TelegramBot.Client.Services.ServiceFactory;

namespace TelegramBot.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            const string BASE_API_URL = "https://localhost:7103/";
            const int TAGS_PER_USER = 1;

            string key = configuration.GetConnectionString("TelegramKey")!;
            TelegramBotClient telegramBot = new TelegramBotClient(key);
            HubConnection hub = new HubConnectionBuilder().WithUrl($"{BASE_API_URL}coursehub")
                                    .Build();

            var builder = ClientBot.CreateBuilder();

            builder.SetTelegramBot(telegramBot);
            builder.SetHubConnection(hub);
            builder.SetCourseServiceFactory<CourseServiceFactory>(BASE_API_URL);
            builder.SetTagServiceFactory<TagServiceFartory>(BASE_API_URL, TAGS_PER_USER);
            builder.SetCommandFactory<CommandFactory>();
            builder.SetNewCourseNotifier<NewCourseNotifier>();

            var bot = builder.Build();
            try
            {
                bot.StartLoopAsync(CancellationToken.None).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine("MAIN LOOP EXIT ERROR - " + ex);
                Thread.Sleep(30000);
            }
        }
    }
}