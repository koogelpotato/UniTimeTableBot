using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Microsoft.Extensions.Logging;
using UniTimeTableBot;


namespace UniTimeTableBot
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger _logger;
        private InlineKeyboardHandler _keyboardHandler = new InlineKeyboardHandler();
        
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
                _ => exception.ToString()
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
                { CallbackQuery: { } callbackQuery } => BotOnCallbackQueryReceived(callbackQuery,cancellationToken),
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
                "/timetable" => SendCourse(_botClient,message,cancellationToken),

                _ => Usage(_botClient, message, cancellationToken)
            };
            Message sentMessage = await action;
            _logger.LogInformation("The message was sent with id:{SentMessageId}", sentMessage.MessageId);

        }
        

        private async Task<Message> SendCourse(ITelegramBotClient botClient,Message message, CancellationToken cancellationToken)
        {
            _keyboardHandler.TransitionTo(new SpecialisationInline(_keyboardHandler, botClient, cancellationToken));
            Scraper scraper = new Scraper(_logger);
            return await _botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: scraper.ReturnWeek("051"),
                replyMarkup:_keyboardHandler.SendInline(),
                cancellationToken: cancellationToken);

        }
        static async Task<Message> Usage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            const string usage = "Usage:\n" +
                                 "/timetable - send timeschedule\n";
                                 
            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: usage,
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
        }
        
        private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            _keyboardHandler.SendInline();
            _keyboardHandler.TransitionWithin();
            await _botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: $"Received: {callbackQuery.Data}",
            replyMarkup: _keyboardHandler.SendInline(),
            cancellationToken: cancellationToken);

        }
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
