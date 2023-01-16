using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTimeTableBot
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek) 
        {
            int diff = (7 + (dateTime.DayOfWeek - dayOfWeek)) % 7;
            return dateTime.AddDays(-1 * diff).Date;
        }
        public static string GetWorkingDays(this DateTime dateTime, DateTime date)
        {
            List<string> workingDays = new List<string>();
            date = date.AddDays(-(date.Date.DayOfWeek.GetHashCode() - 1));
            for (int i = 0; i < 6; i++)
            {
                workingDays.Add(date.AddDays(i).ToShortDateString());
            }
            string combinedString = string.Join(", ", workingDays);
            return combinedString;
        }
    }
}
