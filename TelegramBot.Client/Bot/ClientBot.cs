using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Client.CommandsFactory;
using TelegramBot.Client.Extensions;
using TelegramBot.Client.Services;
using TelegramBot.Client.Services.Interfaces;
using TelegramBot.Shared.DTOs;
using static System.Net.Mime.MediaTypeNames;

namespace TelegramBot.Client.Bot
{
    public class ClientBot
    {
        private readonly TelegramBotClient _bot;
        private readonly HubConnection _hubConnection;
        private readonly INewCourseNotifier _newCourseNotifier;
        private readonly CommandFactory _factory;
        public ClientBot(TelegramBotClient bot,CommandFactory commandFactory, HubConnection hubConnection, INewCourseNotifier newCourseNotifier)
        {
            _bot = bot;
            _factory = commandFactory;
            _hubConnection = hubConnection;
            _newCourseNotifier = newCourseNotifier;

            _hubConnection.On("GetNewAddedCoursesOnClient", (IEnumerable<CourseDto> courses) => _newCourseNotifier.Notify(courses));
        }

        public async Task StartLoopAsync(CancellationToken _cancellationToken)
        {
            await _hubConnection.StartAsync();

            int offset = 0;
            Console.WriteLine("Started at " + DateTime.Now);
            while (!_cancellationToken.IsCancellationRequested)
            {
                var updates = new Update[0];
                try
                {
                    updates = await _bot.GetUpdatesAsync(offset: offset);

                }
                catch (TaskCanceledException)
                {
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR WHILE GETTIGN UPDATES - " + ex);
                }

                foreach (var update in updates)
                {
                    offset = update.Id + 1;
                    ProcessUpdate(_bot, update);
                }

                await Task.Delay(1000);
            }
        }

        private async Task ProcessUpdate(TelegramBotClient bot, Update update)
        {
            try
            {
                var gettingText = update.Message.Text;
                
                string stringCommand = string.Empty;
                string body = string.Empty;
                string replyText = string.Empty;
                ParseMode? parseMode = ParseMode.Markdown;
                Console.WriteLine(update.Message.Chat.Id + " < " + update.Message.From.Username + " - " + gettingText);

                if (gettingText.HasCommand())
                {
                    stringCommand = gettingText.ExtractCommand();
                    body = gettingText.ExtractBody(stringCommand);
                }
                else
                {
                    stringCommand = "NoExistingCommand";
                }

                var command = _factory.Create(stringCommand);

                replyText = await command.Process(body: body, userId: update.Message.Chat.Id, parseMode: parseMode);
;
                await bot.SendTextMessageAsync(update.Message.Chat.Id, replyText, disableWebPagePreview: true, parseMode: parseMode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR - {ex.Message}");
                throw;
            }
        }
    }
}
