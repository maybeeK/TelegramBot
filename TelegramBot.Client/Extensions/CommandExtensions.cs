using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Client.Extensions
{
    public static class CommandExtensions
    {
        public static bool HasCommand(this string text)
        {
            return text != null && text.StartsWith("/");
        }
        public static string ExtractCommand(this string text)
        {
            return text.Replace("/", "").Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
        }
        public static string ExtractBody(this string text, string command)
        {
            return text.Replace($"/{command}", "").Trim();
        }
    }
}
