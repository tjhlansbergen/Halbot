using System;
using System.Globalization;

namespace Halbot.Code
{
    public class HalbotActivity : IComparable<HalbotActivity>
    {
        public int ID { get; set; } // unique id
        public int Distance { get; set; } // in meters
        public string Url { get; set; } // link to tomtom page for activity
        public string Image { get; set; } // link to activity-image
        public DateTime Date { get; set; } // date of activity
        public int Heartrate { get; set; } // average heartrate
        public float Speed { get; set; } // average speed of activity in m/s
        public int Climb { get; set; } // climb in meters
        public int Descent { get; set; } // descent in meters
        public float Lat { get; set; } // location as lat-lng
        public float Lng { get; set; } // location as lat-lng
        public int Duration { get; set; } // active time total in seconds
        public string Description { get; set; } // description for race
        public string Label { get; set; } // a label that catagorizes the activity

        //comparer for sorting (by date)
        public int CompareTo(HalbotActivity other)
        {
            return other.Date.CompareTo(this.Date);
        }

        //Conversion properties
        public int Week
        {
            get { return _weekOfYear(Date); }
        }

        public string Pace
        {
            get { return _paceForSpeed(Speed); }
        }

        public int Effort => (int) Math.Round(((Distance / 1000) + ((Climb + Descent) / 100)) * (Speed / 1.20));

        //helpers
        private static int _weekOfYear(DateTime date)
        {
            var day = (int) CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)),
                CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        private static string _paceForSpeed(double speed)
        {
            string minutes = Math.Floor(1 / (speed * 0.06)).ToString();
            string seconds = Math.Round((60 * ((1 / (speed * 0.06)) - (Math.Floor(1 / (speed * 0.06))))))
                .ToString("00");
            return string.Format("{0}:{1}", minutes, seconds);
        }
    }
}