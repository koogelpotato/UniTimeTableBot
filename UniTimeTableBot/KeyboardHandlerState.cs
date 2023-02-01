using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Microsoft.Extensions.Logging;

namespace UniTimeTableBot
{
    public abstract class KeyboardHandlerState
    {
        protected InlineKeyboardHandler _inlineKeyboardHandler;
        protected ITelegramBotClient _botClient;
        protected CancellationToken _cancellationToken;
        protected KeyboardHandlerState(InlineKeyboardHandler handler,ITelegramBotClient botClient,CancellationToken cancelationToken)
        {
            _botClient = botClient;
            _inlineKeyboardHandler = handler;
            _cancellationToken = cancelationToken;
        }
        public void SetHandler(InlineKeyboardHandler handler)
        {
            this._inlineKeyboardHandler = handler;
        }
        public abstract InlineKeyboardMarkup SendInline();
        public abstract void TransitionWithin();
    }
}
