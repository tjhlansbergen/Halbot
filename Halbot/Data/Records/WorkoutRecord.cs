using System;

namespace Halbot.Data.Records
{
    public class WorkoutRecord
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public int Minutes { get; set; }
        public string Notes { get; set; }

        public int Week => Date.Week();
    }
}
