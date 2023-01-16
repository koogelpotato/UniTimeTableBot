using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UniTimeTableBot
{
    public class Scraper : ISraper,IReturnCurrentDay,IReturnNextDay, IReturnWeek
    {
        private List<Pair> _pairs = new List<Pair>();
        private const string _websiteUrl = "https://sb.bsu.by/raspisanie/map-";
        private ILogger _logger;

        public Scraper(ILogger logger)
        {
            _logger = logger;
        }
        public string Scrape(string? sequence,string date)
        {
            List<Pair> test = new List<Pair>();
            string groupUrl = _websiteUrl + sequence + ".xml";
            var web = new HtmlWeb();
            var doc = web.Load(groupUrl);
            var Rows = doc.DocumentNode.SelectNodes("/html/body/div[6]/div[2]/div/table/tbody/tr");
            string currentDay = "01.09.2022";
            foreach (var pair in Rows)
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
            
            var LinqDataList = test.Where(x => x.Date.Contains(date)).Select(f => new List<string>() { f.Date, f.Time, f.Discipline, f.LectorsName, f.Auditorium }).SelectMany(pair => pair);
            string data = string.Join("\n", LinqDataList);
            return data;
        }

        public string ReturnCurrentDay(string group)
        {
            var dateToReturn = DateTime.Today.Date.ToShortDateString();
            var dateData = Scrape(group, dateToReturn);
            return dateData;
        }

        public string ReturnNextDay(string group)
        {
            var dateToReturn = DateTime.Today.Date.AddDays(1).ToShortDateString();
            var dateData = Scrape(group, dateToReturn);
            return dateData;
        }

        public string ReturnWeek(string group)
        {
            string dates = DateTime.Now.GetWorkingDays(DateTime.Now).ToString();
            return dates;
        }
        
    }
    
}
