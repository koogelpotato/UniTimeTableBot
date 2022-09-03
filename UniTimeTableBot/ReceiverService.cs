using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Abstract;
using Telegram.Bot.Polling;


namespace UniTimeTableBot
{
    public class ReceiverService: ReceiverServiceBase<UpdateHandler> 
    {
        public ReceiverService(
            ITelegramBotClient botClient,
            UpdateHandler updateHandler,
            ILogger<ReceiverServiceBase<UpdateHandler>> logger)
            :base(botClient, updateHandler, logger)
        {

        }
    }
}
