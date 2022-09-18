using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace UniTimeTableBot
{
    internal class HtmlAgilityPackClass : HtmlAgilityPackBase
    {
        public HtmlAgilityPackClass(ILogger logger) : base(logger)
        {

        }
    }
}
