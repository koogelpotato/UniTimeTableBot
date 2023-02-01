using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

public class GroupScraper : IGroupScraper
{
    public List<Group> ScrapeGroups(string url)
    {
        List<Group> groups = new List<Group>();
        var web = new HtmlWeb();
        var doc = web.Load(url);

        var options = doc.DocumentNode.SelectNodes("//select[@class='sch sch-0 sch-group']/option");
        Console.WriteLine(options);
        foreach (var option in options)
        {
            var parts = option.InnerText.Split("/");
            var group = new Group
            {
                GroupLink = option.Attributes["value"].Value,
                GroupCourse = parts[0],
                GroupNumber = parts[1]
            };
            groups.Add(group);
        }
        return groups;
    }

}
