﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Logging;

namespace Telegram.Bot.Abstract
{
    
    public abstract class ReceiverServiceBase<TUpdateHandler> : IReceiverService
        where TUpdateHandler : IUpdateHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IUpdateHandler _updateHandler;
        private readonly ILogger<ReceiverServiceBase<TUpdateHandler>> _logger;

        public ReceiverServiceBase(
            ITelegramBotClient botClient,
            TUpdateHandler updateHandler,
            ILogger<ReceiverServiceBase<TUpdateHandler>> logger)
        {
            _botClient = botClient;
            _updateHandler = updateHandler;
            _logger = logger;
        }
        public async Task ReceiveAsync(CancellationToken stoppingToken)
        {
            var receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = Array.Empty<UpdateType>(),
                ThrowPendingUpdates = true,
            };
            var me = await _botClient.GetMeAsync(stoppingToken);
            _logger.LogInformation("Start receive updates for {BotName}", me.Username ?? "My Awesome Bot");

            await _botClient.ReceiveAsync(
                updateHandler: _updateHandler,
                receiverOptions: receiverOptions,
                cancellationToken: stoppingToken);
        }
    }
}
