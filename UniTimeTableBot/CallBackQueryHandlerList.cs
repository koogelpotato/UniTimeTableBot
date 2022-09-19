using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace UniTimeTableBot
{
    public class CourseHandler : AbstractQueryHandler
    {
        public override string Handle(string callBackQuery)
        {
            if (callBackQuery.ToString() == "1"|| callBackQuery.ToString() == "2" || callBackQuery.ToString() == "3" || callBackQuery.ToString() == "4")
            {
                return callBackQuery.ToString(); 
            }
            else
            {
                return base.Handle(callBackQuery);
            }
        }
        
    }
    public class SpecialisationHandler : AbstractQueryHandler
    {
        public override string Handle(string callBackQuery)
        {
            if (callBackQuery.ToString() == "Бизнес-администрирование" || callBackQuery.ToString() == "Логистика" || callBackQuery.ToString() == "Маркетинг" || callBackQuery.ToString() == "Менеджмент" || callBackQuery.ToString() == "УИР")
            {
                return callBackQuery.ToString();
            }
            else
            {
                return base.Handle(callBackQuery);
            }
        }
    }
}
