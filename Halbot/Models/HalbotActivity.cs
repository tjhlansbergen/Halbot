using System;
using System.Globalization;

namespace Halbot.Models
{
    public class HalbotActivity : IComparable<HalbotActivity>
    {
        public long Id { get; set; } // unique id
        public double Distance { get; set; } // in meters
        public Uri Url { get; set; } // link to tomtom/garmin page for activity
        public DateTime Date { get; set; } // date of activity
        public double Heartrate { get; set; } // average heartrate
        public double Cadence { get; set; } // average cadence
        public double TrainingEffect { get; set; }
        public double AnaerobicTrainingEffect { get; set; }
        public double Speed { get; set; } // average speed of activity in m/s
        public double Climb { get; set; } // climb in meters
        public double Descent { get; set; } // descent in meters
        public double MaxElevation { get; set; }
        public double MinElevation { get; set; }
        public double Lat { get; set; } // location as lat-lng
        public double Lng { get; set; } // location as lat-lng
        public double Duration { get; set; } // active time total in seconds
        public string Description { get; set; } // description of the activity
        public bool IsRace { get; set; }
        public ActivityDataType DataType { get; set; }
        public string Journal { get; set; }

        //comparer for sorting (by date)
        public int CompareTo(HalbotActivity other)
        {
            return other.Date.CompareTo(this.Date);
        }

        //Conversion properties
        public int Week => Date.Week();
        public string Pace => PaceForSpeed(Speed);

        public int Effort => (int) Math.Round(((Distance + (Climb * 8)) * Speed) / 1000);

        public static string PaceForSpeed(double speed)
        {
            string minutes = Math.Floor(1 / (speed * 0.06)).ToString(CultureInfo.InvariantCulture);
            string seconds = Math.Round((60 * ((1 / (speed * 0.06)) - (Math.Floor(1 / (speed * 0.06))))))
                .ToString("00");
            return $"{minutes}:{seconds}";
        }
    }
}