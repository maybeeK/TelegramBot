using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using TelegramBot.Client.Bot;
using TelegramBot.Client.CommandsFactory;
using TelegramBot.Client.CommandsFactory.Abstract;
using TelegramBot.Client.Services;
using TelegramBot.Client.Services.Interfaces;
using TelegramBot.Client.Services.ServiceFactory;
using TelegramBot.Client.Services.ServiceFactory.Abstact;

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

            CourseServiceFactoryBase courseServiceFactory = new CourseServiceFactory(baseApiUrl: BASE_API_URL);
            TagServiceFactoryBase tagServiceFactory = new TagServiceFartory(baseApiUrl: BASE_API_URL, tagsPerUser: TAGS_PER_USER);

            CommadFactoryBase factory = new CommandFactory(courseServiceFactoryBase: courseServiceFactory, tagServiceFactoryBase: tagServiceFactory);
            HubConnection hub = new HubConnectionBuilder().WithUrl($"{BASE_API_URL}coursehub")
                                    .Build();

            INewCourseNotifier newCourseNotifier = new NewCourseNotifier(telegramBot, tagServiceFactory.CreateTagService());

            try
            {
                ClientBot bot = new ClientBot(bot: telegramBot,
                                          commandFactory: factory,
                                          hubConnection: hub,
                                          newCourseNotifier: newCourseNotifier);

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