using Halbot.Data.Records;
using System.Collections.Generic;
using System.Linq;

namespace Halbot.Data
{
    public static class WorkoutCache
    {
        private static List<WorkoutRecord> _workouts = new List<WorkoutRecord>();

        public static List<WorkoutRecord> Get(DatabaseContext context)
        {
            if (_workouts.Count != context.WorkoutRecords.Count())
            {
                _workouts = context.WorkoutRecords.ToList();
            }

            return _workouts;
        }

        public static void InvalidateCache()
        {
            _workouts.Clear();
        }
    }
}