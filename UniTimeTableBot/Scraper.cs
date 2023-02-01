using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UniTimeTableBot
{
    public class Scraper : IScraper
    {
        private ILogger _logger;

        public Scraper(ILogger logger)
        {
            _logger = logger;
        }
        public string Scrape(string? url)
        {
            List<Pair> _pairs = new List<Pair>();
            var web = new HtmlWeb();
            var doc = web.Load(url);
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
                    _pairs.Add(newPair);
                }
            }
            var LinqDataList = _pairs.Where(x => x.Date.Contains(currentDay)).Select(f => new List<string>() { f.Date, f.Time, f.Discipline, f.LectorsName, f.Auditorium }).SelectMany(pair => pair);
            string data = string.Join("\n", LinqDataList);
            return data;
        }
    }
    
}
