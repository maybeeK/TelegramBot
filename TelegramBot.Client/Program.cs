using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Net.Http.Json;
using Telegram.Bot;
using TelegramBot.Client.Bot;
using TelegramBot.Client.CommandsFactory;
using TelegramBot.Client.Services;
using TelegramBot.Client.Services.Interfaces;
using TelegramBot.Client.Services.ServiceFactory;
using TelegramBot.Shared.DTOs;

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

            string key = configuration.GetConnectionString("TelegramKey")!;
            TelegramBotClient telegramBot = new TelegramBotClient(key);
            CommandFactory factory = new CommandFactory();
            HubConnection hub = new HubConnectionBuilder().WithUrl("https://localhost:7103/coursehub")
                                    .Build();
            INewCourseNotifier newCourseNotifier = new NewCourseNotifier(telegramBot, TagServiceFartory.GetTagService<TagService>());

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