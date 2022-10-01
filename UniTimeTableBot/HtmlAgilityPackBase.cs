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
            List<Pair> test = new List<Pair>();
            string groupUrl = _websiteUrl + sequence + ".xml";
            var web = new HtmlWeb();
            var doc = web.Load(groupUrl);
            var LatestWeek = doc.DocumentNode.SelectNodes("/html/body/div[6]/div[2]/div/div[1]/span").Last().InnerText;
            var CurrentWeek = doc.DocumentNode.SelectNodes("//tr[@vl = '"+LatestWeek+"']");
            var currentDay = LatestWeek;
            foreach (var pair in CurrentWeek)
            {
                if(pair.SelectSingleNode("td").HasClass("head-date"))
                {
                    currentDay = pair.SelectSingleNode("td").InnerText;
                }
                else
                {
                    Pair newPair = new Pair
                    {
                        Date = currentDay,
                        Time = pair.SelectSingleNode("td[1]").InnerText,
                        Discipline = pair.SelectSingleNode("td[2]").InnerText,
                        LectorsName = pair.SelectSingleNode("td[3]").InnerText,
                        Auditorium = pair.SelectSingleNode("td[4]").InnerText,
                    };
                    test.Add(newPair);
                }
                
            }
            var dateToReturn = DateTime.Today.Date.ToShortDateString();
            var LinqDataList = test.Where(x => x.Date.Contains(dateToReturn)).Select(f => new List<string>() { f.Date, f.Time, f.Discipline, f.LectorsName, f.Auditorium }).SelectMany(pair => pair);
            string day = string.Join("\n", LinqDataList);
            return day;
        }

         
    }
    
}
