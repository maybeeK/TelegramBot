﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Interfaces;

namespace TelegramBot.Client.Commands
{
    public class HelpCommand : ICommand
    {
        public Task<string> Proccess(string? body = null, long? userId = null, ParseMode? parseMode = null)
        {
            return Task.FromResult(
                $"/start - to start\n" +
                $"/help - all commands and usage\n" +
                $"other...."
            );
        }
    }
}
