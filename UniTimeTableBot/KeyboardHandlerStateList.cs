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
    public class CourseInline : KeyboardHandlerState
    {
        public CourseInline(InlineKeyboardHandler handler, ITelegramBotClient botClient, CancellationToken cancelationToken) : base(handler, botClient, cancelationToken)
        {
        }

        public override InlineKeyboardMarkup SendInline()
        {

            InlineKeyboardMarkup inlineKeyboardMarkup = new(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("1","1"),
                    InlineKeyboardButton.WithCallbackData("2","2"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("3","3"),
                    InlineKeyboardButton.WithCallbackData("4","4")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("5","5")
                },

            });
            
            return inlineKeyboardMarkup;
            
        }

        public override void TransitionWithin()
        {
            _inlineKeyboardHandler.TransitionTo(new SpecialisationInline(_inlineKeyboardHandler, _botClient, _cancellationToken));
        }
    }
    public class SpecialisationInline : KeyboardHandlerState
    {
        public SpecialisationInline(InlineKeyboardHandler handler, ITelegramBotClient botClient, CancellationToken cancelationToken) : base(handler, botClient, cancelationToken)
        {
        }

        public override InlineKeyboardMarkup SendInline()
        {
            InlineKeyboardMarkup inlineKeyboardMarkup = new(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Бизнес-администрирование","Бизнес-администрирование"),
                    InlineKeyboardButton.WithCallbackData("Логистика","Логистика"),
                    InlineKeyboardButton.WithCallbackData("Маркетинг","Маркетинг")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Менеджмент","Менеджмент"),
                    InlineKeyboardButton.WithCallbackData("💪УИР💪","УИР")
                }

            });
            return inlineKeyboardMarkup;
        }

        public override void TransitionWithin()
        {
            _inlineKeyboardHandler.TransitionTo(new NullInline(_inlineKeyboardHandler, _botClient, _cancellationToken));
        }
    }
    public class GroupInline : KeyboardHandlerState
    {
        public GroupInline(InlineKeyboardHandler handler, ITelegramBotClient botClient, CancellationToken cancelationToken) : base(handler, botClient, cancelationToken)
        {
        }

        public override InlineKeyboardMarkup SendInline()
        {
            throw new NotImplementedException();
        }

        public override void TransitionWithin()
        {
            throw new NotImplementedException();
        }
    }
    public class NullInline : KeyboardHandlerState
    {
        public NullInline(InlineKeyboardHandler handler, ITelegramBotClient botClient, CancellationToken cancelationToken) : base(handler, botClient, cancelationToken)
        {
        }

        public override InlineKeyboardMarkup SendInline()
        {
            return null;
        }

        public override void TransitionWithin()
        {
            
        }
    }

}
