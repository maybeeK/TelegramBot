using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Net.Http.Json;
using TelegramBot.Client.Bot;
using TelegramBot.Client.CommandsFactory;
using TelegramBot.Client.Services;
using TelegramBot.Client.Services.Interfaces;
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

            string key = configuration.GetConnectionString("TelegramKey");
            CommandFactory factory = new CommandFactory();
            HubConnection hub = new HubConnectionBuilder().WithUrl("https://localhost:7103/coursehub")
                                    .Build();
            
            try
            {
                ClientBot bot = new ClientBot(key: key,
                                          commandFactory: factory,
                                          hubConnection: hub);

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