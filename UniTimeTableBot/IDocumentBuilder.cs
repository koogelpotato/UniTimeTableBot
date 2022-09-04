namespace UniTimeTableBot
{
    public interface IDocumentBuilder
    {
        public Task ReturnSchedule(CancellationToken cancellationToken);
    }
}