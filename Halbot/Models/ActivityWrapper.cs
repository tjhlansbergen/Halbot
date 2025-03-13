using System;
using Halbot.Data.Records;

namespace Halbot.Models
{
    public class ActivityWrapper
    {
        public WrappedType Type { get; set; }
        public HalbotActivity HalbotActivity { get; set; }
        public WorkoutRecord WorkoutRecord { get; set; }
        public DateTime Date { get; set; }
    }
}
