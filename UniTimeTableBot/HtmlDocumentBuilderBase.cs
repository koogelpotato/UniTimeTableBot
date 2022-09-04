using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace UniTimeTableBot
{
    public class HtmlDocumentBuilderBase : IDocumentBuilder
    {
        private readonly string _groupNumber;
        private const string _url = "https://sb.bsu.by/raspisanie/map-";
        public HtmlDocumentBuilderBase(string groupNumber, string url)
        {
            _groupNumber = groupNumber;
        }
        private string ScheduleBuilder()
        {
            string groupUrl = _url + _groupNumber;
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(groupUrl);

        }
        public Task ReturnSchedule(CancellationToken cancellationToken)
        {
            
        }
    }
}
