using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Polling;
using Telegram.Bot;
using System.IO;

namespace UniTimeTableBot
{
    public abstract class HtmlAgilityPackBase : ISraper
    {
        private List<Pair> _pairs = new List<Pair>();
        private const string _websiteUrl = "https://sb.bsu.by/raspisanie/map-";
        private ILogger _logger;

        public HtmlAgilityPackBase(ILogger logger)
        {
            _logger = logger;
        }
        public string Scrape(string? sequence)
        {
            List<string> pairs = new List<string>();
            string groupUrl = _websiteUrl + sequence + ".xml";
            var web = new HtmlWeb();
            var doc = web.Load(groupUrl);
            var LatestWeek = doc.DocumentNode.SelectNodes("/html/body/div[6]/div[2]/div/div[1]/span").Last().InnerText;
            _logger.LogCritical(LatestWeek);
            var CurrentWeek = doc.DocumentNode.SelectNodes("//tr[@vl = '"+LatestWeek+"']");

            foreach (var pair in CurrentWeek)
            {
                pairs.Add(pair.InnerText);
            }
            string week = string.Join(" ", pairs);
            _logger.LogCritical(week);
            return week;
        }
        
            
    }
}
