using Telegram.Bot.Types;
namespace UniTimeTableBot
{
    public class CallbackQueryHandle
    {
        
        public static string CallBackQueryCode(AbstractQueryHandler handler, CallbackQuery query)
        {
            var result = handler.Handle(query?.Data);
            if(result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}