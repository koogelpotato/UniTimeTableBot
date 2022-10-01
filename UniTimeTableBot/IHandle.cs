namespace UniTimeTableBot
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        QueryRequest Handle(QueryRequest callBackQuery);
    }
}