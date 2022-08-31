using System;
using Telegram.Bot;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Extensions.Polling;
namespace UniTimeTableBot
{
   class Program
   {
        static TelegramBotClient bot = new TelegramBotClient("5358505929:AAEy4tXCsSxt7GAOPlAJ2fkuWkVGm1Ka_Ag");
        static readonly ILogger<Update> logger;
        static void Main(string[] args)
        {
            UpdateHandler updateHandler = new UpdateHandler(bot,logger);
            bot.StartReceiving(updateHandler,);
        }
   }

}
