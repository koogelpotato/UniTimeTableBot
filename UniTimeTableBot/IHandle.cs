namespace UniTimeTableBot
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        string Handle(string callBackQuery);
    }
}