﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using TelegramBot.Client.Commands.Interfaces;

namespace TelegramBot.Client.Commands
{
    public class StartCommand:ICommand
    {
        public Task<string> Proccess(string? body = null, long? userId = null, ParseMode? parseMode = null)
        {
            return Task.FromResult($"Hello! I am Bot!\n/help - for more commands!");
        }
    }
}