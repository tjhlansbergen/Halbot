using System;
using System.Globalization;

namespace Halbot
{
    public static class Extentions
    {
        public static int Week(this DateTime dateTime)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(dateTime);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dateTime.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
