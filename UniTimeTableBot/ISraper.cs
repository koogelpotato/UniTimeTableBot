namespace UniTimeTableBot
{
    internal interface ISraper
    {
        List<string> Scrape(string groupNumber);
    }
}