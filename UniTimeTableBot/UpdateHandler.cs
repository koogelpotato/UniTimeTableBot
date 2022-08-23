using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Microsoft.Extensions.Logging;
namespace UniTimeTableBot
{
    public class UpdateHandler
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
    }
}
