using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Net.Http.Json;
using TelegramBot.Client.Bot;
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

            ClientBot bot = new(key);

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