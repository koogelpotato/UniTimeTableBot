using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Microsoft.Extensions.Logging;
using HtmlAgilityPack;
using System.Xml;
using System.Linq;

namespace UniTimeTableBot
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger _logger;

        public UpdateHandler(ITelegramBotClient botClient, ILogger<Update> logger)
        {
            _botClient = botClient;
            _logger = logger;
        }
        public Task PollingErrorHandler(ITelegramBotClient _, Exception exception, CancellationToken cancellationToken = default)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _                                     => exception.ToString()
            };
            _logger.LogInformation("HandleError: {Error Message}", ErrorMessage);
            return Task.CompletedTask;
        }
        public async Task HandleUpdateAsync(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
        {
            var handler = update switch
            {
                { Message: { } message } => BotOnMessageReceived(message, cancellationToken),
                { EditedMessage: { } message } => BotOnMessageReceived(message, cancellationToken),
                { CallbackQuery: { } callbackQuery } => BotOnCallbackQueryReceived(callbackQuery, cancellationToken),
                { InlineQuery: { } inlineQuery } => BotOnInlineQueryReceived(inlineQuery, cancellationToken),
                { ChosenInlineResult: { } chosenInlineResult } => BotOnChoosenInlineResultReceived(chosenInlineResult, cancellationToken),
                _ => UnknownUpdateHandlerAsync(update, cancellationToken)
            };
            await handler;
        }
        private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Receive message type:{MessageType}", message.Type);
            if (message.Text is not { } messageText)
                return;
            var action = messageText.Split(' ')[0] switch
            {
                "/inline_keyboard" => SendInlineKeyboard(_botClient, message, cancellationToken),
                "/keyboard" => SendReplyKeyboard(_botClient, message, cancellationToken),
                "/remove" => RemoveKeyboard(_botClient, message, cancellationToken),
                "/god" => SendFile(_botClient, message, cancellationToken),
                "/request" => RequestContactAndLocation(_botClient, message, cancellationToken),
                "/inline_mode" => StartInlineQuery(_botClient, message, cancellationToken),


                _ => Usage(_botClient, message, cancellationToken)
            };
            Message sentMessage = await action;
            _logger.LogInformation("The message was sent with id:{SentMessageId}", sentMessage.MessageId);

        }
        private async Task<Message> SendInlineKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            HtmlAgilityPackClass puller = new HtmlAgilityPackClass(_logger);

            await botClient.SendChatActionAsync(
                chatId: message.Chat.Id,
                chatAction: ChatAction.Typing,
                cancellationToken: cancellationToken);
            
            await Task.Delay(500, cancellationToken);
            InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("211","211"),
                        InlineKeyboardButton.WithCallbackData("212","212"),
                        InlineKeyboardButton.WithCallbackData("213","213__engl_"),
                        InlineKeyboardButton.WithCallbackData("214","214__engl_"),
                        InlineKeyboardButton.WithCallbackData("215","215__engl_"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("1211","1211_z_o"),
                        InlineKeyboardButton.WithCallbackData("1212","1212_z_o"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("011","011"),
                        InlineKeyboardButton.WithCallbackData("012","012"),
                        InlineKeyboardButton.WithCallbackData("013","013__engl_"),
                        InlineKeyboardButton.WithCallbackData("014","014__engl_"),
                        InlineKeyboardButton.WithCallbackData("015","015__engl_"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("3011","3011_ba"),
                        
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("911","911"),
                        InlineKeyboardButton.WithCallbackData("912","912"),
                        InlineKeyboardButton.WithCallbackData("913","913__engl_"),
                        InlineKeyboardButton.WithCallbackData("914","914__engl_"),
                        InlineKeyboardButton.WithCallbackData("915","915__engl_"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("3911","3911_ba"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("221","221"),
                        InlineKeyboardButton.WithCallbackData("222","222"),
                        InlineKeyboardButton.WithCallbackData("223","223__engl_"),
                        InlineKeyboardButton.WithCallbackData("224","224__engl_"),
                        InlineKeyboardButton.WithCallbackData("225","225__engl_"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("1221","1221_z_o"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("121","121"),
                        InlineKeyboardButton.WithCallbackData("122","122"),
                        InlineKeyboardButton.WithCallbackData("123","123__engl_"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("021","021"),
                        InlineKeyboardButton.WithCallbackData("022","022"),
                        InlineKeyboardButton.WithCallbackData("023","023__engl_"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("921","921"),
                        InlineKeyboardButton.WithCallbackData("922","922"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("231","231"),
                        InlineKeyboardButton.WithCallbackData("232","232"),
                        InlineKeyboardButton.WithCallbackData("233","233__engl_"),
                        InlineKeyboardButton.WithCallbackData("234","234__engl_"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("1231","1231_z_o"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("131","131"),
                        InlineKeyboardButton.WithCallbackData("132","132"),
                        InlineKeyboardButton.WithCallbackData("133","133__engl_"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("031","031_ek"),
                        InlineKeyboardButton.WithCallbackData("032","032_rd"),
                        InlineKeyboardButton.WithCallbackData("033","033__engl_"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("931","931_ek"),
                        InlineKeyboardButton.WithCallbackData("932","932_rd"),
                        InlineKeyboardButton.WithCallbackData("933","933__engl_"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("101","101"),
                        InlineKeyboardButton.WithCallbackData("102","102"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("001","001_fin"),
                        InlineKeyboardButton.WithCallbackData("002","002_sa"),
                        InlineKeyboardButton.WithCallbackData("901","901_fin"),
                        InlineKeyboardButton.WithCallbackData("902","902_sa"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("251","251"),
                        InlineKeyboardButton.WithCallbackData("252","252"),
                        InlineKeyboardButton.WithCallbackData("253","253__engl_"),
                        InlineKeyboardButton.WithCallbackData("254","254__engl_"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("1251","1251"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("251","251"),
                        InlineKeyboardButton.WithCallbackData("252","252"),
                        InlineKeyboardButton.WithCallbackData("253","253__engl_"),
                        InlineKeyboardButton.WithCallbackData("254","254__engl_"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("151","151"),
                        InlineKeyboardButton.WithCallbackData("152","152"),
                        InlineKeyboardButton.WithCallbackData("153","153__engl_"),
                        InlineKeyboardButton.WithCallbackData("154","154__engl_"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("051","051"),
                        InlineKeyboardButton.WithCallbackData("052","052"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("951","951"),
                        InlineKeyboardButton.WithCallbackData("952","952"),
                    },
                });
            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Введите номер группы", 
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
             

        }
        static async Task<Message> SendReplyKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(
                new[]
                {
                    new KeyboardButton[]{ "211", "212"},
                    new KeyboardButton[]{ "213__engl_", "214__engl_","215__engl_"},
                })
            {
                ResizeKeyboard = true
            };

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Введите номер группы",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken:cancellationToken);
        }
        static async Task<Message> RemoveKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Removing Keyboard",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken:cancellationToken);
        }
        static async Task<Message> SendFile(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await botClient.SendChatActionAsync(
                message.Chat.Id,
                ChatAction.UploadPhoto,
                cancellationToken: cancellationToken);
            const string filePath = @"Photo/billy.png";
            await using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var fileName = filePath.Split(Path.DirectorySeparatorChar).Last();
            return await botClient.SendPhotoAsync(
                chatId: message.Chat.Id,
                photo: new InputOnlineFile(fileStream, fileName),
                caption: "Nice picture",
                cancellationToken: cancellationToken);
        }
        static async Task<Message> RequestContactAndLocation(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            ReplyKeyboardMarkup RequestReplyKeyboard = new(
                new[]
                {
                    KeyboardButton.WithRequestLocation("Location"),
                    KeyboardButton.WithRequestContact("Contact"),
                });
            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Who or where are you?",
                replyMarkup: RequestReplyKeyboard,
                cancellationToken: cancellationToken);
        }
        static async Task<Message> Usage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            const string usage = "Usage:\n" +
                                 "/inline_keyboard - send inline kayboard\n" +
                                 "/keyboard  - send custom keyboard\n" +
                                 "/remove - remove custom keyboard\n" +
                                 "/god - send a photo\n" +
                                 "/request - request location or contact\n" +
                                 "/inline_mode - send keyboard with Inline query\n" +
                                 "/return_pairs - return today's pairs";
            return await botClient.SendTextMessageAsync(
                chatId:message.Chat.Id,
                text: usage,
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
        }
        static async Task<Message> StartInlineQuery(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            InlineKeyboardMarkup inlineKeyboard = new(
                InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("Inline Mode"));

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Press the button to start Inline Query",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }
        private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received inline kayboard callback from:{CallbackQueryId}", callbackQuery.Id);
            _logger.LogCritical("Received inline kayboard callback from:{CallbackQueryData}", callbackQuery.Data);
            HtmlAgilityPackClass htmlAgilityPack = new HtmlAgilityPackClass(_logger);
            await _botClient.AnswerCallbackQueryAsync(
                callbackQueryId: callbackQuery.Id,
                text: $"Received {callbackQuery.Data}",
                cancellationToken: cancellationToken);
            await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: $"Пары на неделю: {htmlAgilityPack.Scrape(callbackQuery.Data)}",
            cancellationToken: cancellationToken);


        }
        #region InlineMode
        private async Task BotOnInlineQueryReceived(InlineQuery inlineQuery, CancellationToken cancellationToken)
        {
            InlineQueryResult[] results =
            {
                new InlineQueryResultArticle(
                    id:"1",
                    title:"TgBots",
                    inputMessageContent: new InputTextMessageContent("hello")),
                
            };
            await _botClient.AnswerInlineQueryAsync(
                inlineQueryId: inlineQuery.Id,
                results: results,
                cacheTime: 0,
                cancellationToken: cancellationToken);
        }
        private async Task BotOnChoosenInlineResultReceived(ChosenInlineResult chosenInlineResult, CancellationToken cancellationToken)
        {
            _logger.LogCritical("Received inline result: {ChosenInlineResultId}", chosenInlineResult.ResultId);

            await _botClient.SendTextMessageAsync(
                chatId: chosenInlineResult.From.Id,
                text: $"You chose result with Id:{chosenInlineResult.ResultId}",
                cancellationToken: cancellationToken);
        }
        #endregion
        private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unknown update type:{UpdateType}", update.Type);
            return Task.CompletedTask;
        }


        public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API error\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            _logger.LogInformation("HandleError: {ErrorMessage}", ErrorMessage);
            if (exception is RequestException)
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }

    }
}
