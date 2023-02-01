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
    public class InlineKeyboardHandler
    {
        public KeyboardHandlerState _state { get;  set; }
        public void TransitionTo(KeyboardHandlerState state)
        {
            this._state = state;
            this._state.SetHandler(this);
        }
        public InlineKeyboardMarkup SendInline()
        {
            if (_state != null)
                return this?._state.SendInline();
            else
                return null;
        }
        public void TransitionWithin()
        {
            if(_state != null)
            this._state.TransitionWithin();
        }
        
    }
}
