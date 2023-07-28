using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.API.CourseParsers;
using TelegramBot.API.Entities;
using TelegramBot.Client.Commands;
using TelegramBot.Client.Commands.Interfaces;
using TelegramBot.Client.CommandsFactory;
using TelegramBot.Client.Extensions;
using TelegramBot.Client.Services.ServiceFactory;

namespace TelegramBot.Tests
{
    public class BotCommandsTests
    {

        [Fact]
        public void CommonExtractCommandTest()
        {
            // Arrange
            string userText = "/start 123";

            // Act
            string command = userText.ExtractCommand();
            

            // Assert
            Assert.Equal("start", command);
        }

        [Fact]
        public void CommonExtractBodyTest()
        {
            // Arrange
            string body = "123 321";
            string command = "start";
            string userText = $"/{command} {body}";

            // Act
            string extractedBody = userText.ExtractBody(command);

            // Assert
            Assert.Equal(body, extractedBody);
        }

        [Fact]
        public void HasCommandTest()
        {
            // Arrange
            string userText = "/start 123";

            // Act
            bool command = userText.HasCommand();
            

            // Assert
            Assert.True(command);
        }

        [Fact]
        public void StartCommandFactoryTest()
        {
            // Arrange
            string strCommand = "start";
            CommandFactory factory = new CommandFactory(new TagServiceFartory("https://existing/link/", 1), new CourseServiceFactory("https://existing/link/"));

            // Act
            ICommand command = factory.Create(strCommand);

            // Assert
            Assert.Equal(typeof(StartCommand), command.GetType());
        }

        [Fact]
        public void HelpCommandFactoryTest()
        {
            // Arrange
            string strCommand = "help";
            CommandFactory factory = new CommandFactory(new TagServiceFartory("https://existing/link/", 1), new CourseServiceFactory("https://existing/link/"));

            // Act
            ICommand command = factory.Create(strCommand);

            // Assert
            Assert.Equal(typeof(HelpCommand), command.GetType());
        }

        [Fact]
        public void NoExistingCommandFactoryTest()
        {
            // Arrange
            string strCommand = "itDoesntExist";
            CommandFactory factory = new CommandFactory(new TagServiceFartory("https://existing/link/", 1), new CourseServiceFactory("https://existing/link/"));

            // Act
            ICommand command = factory.Create(strCommand);

            // Assert
            Assert.Equal(typeof(NoExistingCommand), command.GetType());
        }
    }
}
