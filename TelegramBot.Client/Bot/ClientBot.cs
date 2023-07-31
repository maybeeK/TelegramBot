using Microsoft.AspNetCore.SignalR.Client;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Client.CommandsFactory.Abstract;
using TelegramBot.Client.Extensions;
using TelegramBot.Client.Services.Interfaces;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.Client.Bot
{
    public class ClientBot
    {
        private const int MAX_PARALLEL_PROCESSING = 10;
        private readonly TelegramBotClient _bot;
        private readonly HubConnection _hubConnection;
        private readonly INewCourseNotifier _newCourseNotifier;
        private readonly CommadFactoryBase _factory;
        public static ClientBotBuilder CreateBuilder() => new ClientBotBuilder();
        internal ClientBot(TelegramBotClient bot , CommadFactoryBase commandFactory, HubConnection hubConnection, INewCourseNotifier newCourseNotifier)
        {
            _bot = bot;
            _factory = commandFactory;
            _hubConnection = hubConnection;
            _newCourseNotifier = newCourseNotifier;

            _hubConnection.On("GetNewAddedCoursesOnClient", (IEnumerable<CourseDto> courses) => _newCourseNotifier.Notify(courses));
        }

        public async Task StartLoopAsync(CancellationToken _cancellationToken)
        {
            Console.WriteLine("Started at " + DateTime.Now);
            await _hubConnection.StartAsync();

            int offset = 0;
            while (!_cancellationToken.IsCancellationRequested)
            {
                var updates = Enumerable.Empty<Update>();
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

                var opts = new ParallelOptions() { MaxDegreeOfParallelism = MAX_PARALLEL_PROCESSING };
                await Parallel.ForEachAsync(updates, opts, async (update, _) =>
                {
                    offset= ++update.Id;
                    await ProcessUpdate(_bot, update);
                });

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
                ParseMode? parseMode = null;

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

                replyText = await command.Process(body: body, userId: update.Message.Chat.Id, parseMode: ref parseMode);
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
