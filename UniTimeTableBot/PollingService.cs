using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Abstract;

namespace UniTimeTableBot
{
    public class PollingService : PollingServiceBase<ReceiverService>
    {
        public  PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
            : base(serviceProvider, logger)
        {

        }
    }
}
