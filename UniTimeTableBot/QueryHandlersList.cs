using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace UniTimeTableBot
{
    public class CourseHandler : AbstractQueryHandler
    {
        public override QueryRequest Handle(QueryRequest callBackQuery)
        {
            if(callBackQuery.Course != null)
            {
                callBackQuery.Course = 
            }
            else
            {
                return base.Handle(callBackQuery);
            }
        }
    }
    public class SpecialisationHandler:AbstractQueryHandler
    {

    }
    public class GroupHandler : AbstractQueryHandler
    {
        public override QueryRequest Handle(QueryRequest callBackQuery)
        {

            return base.Handle(callBackQuery);
        }
    }
    public class DayHandler : AbstractQueryHandler
    {
        public override QueryRequest Handle(QueryRequest callBackQuery)
        {

            return base.Handle(callBackQuery);
        }
    }
}
