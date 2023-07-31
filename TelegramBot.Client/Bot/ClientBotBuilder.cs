using Microsoft.AspNetCore.SignalR.Client;
using Telegram.Bot;
using TelegramBot.Client.CommandsFactory.Abstract;
using TelegramBot.Client.Services.Interfaces;
using TelegramBot.Client.Services.ServiceFactory;

namespace TelegramBot.Client.Bot
{
    public class ClientBotBuilder
    {
        private TelegramBotClient? _botClient;
        private CourseServiceFactory? _courseServiceFactory;
        private TagServiceFartory? _tagServiceFartory;
        private CommadFactoryBase? _commadFactory;
        private HubConnection? _hub;
        private INewCourseNotifier? _newCourseNotifier;

        public void SetTelegramBot(TelegramBotClient telegramBot)
        {
            _botClient = telegramBot;
        }
        public void SetCourseServiceFactory<T>(params object?[]? args) where T : CourseServiceFactory
        {
            _courseServiceFactory = Activator.CreateInstance(typeof(T), args) as CourseServiceFactory;
        }
        public void SetTagServiceFactory<T>(params object?[]? args) where T : TagServiceFartory
        {
            _tagServiceFartory = Activator.CreateInstance(typeof(T), args) as TagServiceFartory;
        }
        public void SetHubConnection(HubConnection hubConnection)
        {
            _hub = hubConnection;
        }
        public void SetCommandFactory<T>(params object?[]? args) where T : CommadFactoryBase
        {
            if (_courseServiceFactory is null || _tagServiceFartory is null)
            {
                throw new ArgumentNullException();
            }

            _commadFactory = Activator.CreateInstance(typeof(T), _tagServiceFartory, _courseServiceFactory) as CommadFactoryBase;
        }
        public void SetNewCourseNotifier<T>(params object?[]? args) where T : class, INewCourseNotifier
        {
            if (_botClient is null || _tagServiceFartory is null)
            {
                throw new ArgumentNullException();
            }
            _newCourseNotifier = Activator.CreateInstance(typeof(T), _botClient, _tagServiceFartory.CreateTagService()) as INewCourseNotifier;
        }
        public ClientBot Build()
        {
            if (CanNotBotBeCreated())
            {
                throw new ArgumentNullException();
            }
            return new ClientBot(bot: _botClient, 
                                 commandFactory: _commadFactory,
                                 hubConnection: _hub,
                                 newCourseNotifier: _newCourseNotifier);
        }
        private bool CanNotBotBeCreated()
        {
            return _botClient is null || _commadFactory is null || _hub is null || _newCourseNotifier is null;
        }
    }
}
