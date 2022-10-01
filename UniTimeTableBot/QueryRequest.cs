using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTimeTableBot
{
    public class QueryRequest
    {
        public string Course { get; set; } = null!;
        public string Specialisation { get; set; } = null!;
        public string Group { get; set;} = null!;
        public DayToReturnTo returnTo = DayToReturnTo.Today;
    }
    public enum DayToReturnTo
    {
        Today,
        Tommorow,
    }
}
