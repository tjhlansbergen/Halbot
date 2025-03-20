using System.Collections.Generic;
using Halbot.Data.Records;

namespace Halbot.Models
{
    public class IndexModel
    {
        //properties
        public List<HalbotActivity> Activities { get; private set; }
        public List<WorkoutRecord> Workouts { get; private set; }
        public List<ActivityWrapper> CombinedData { get; private set; }
    

        //constructor
        public IndexModel(List<HalbotActivity> activities, List<WorkoutRecord> workouts)
        {
            //initialize
            Activities = activities;
            Workouts = workouts;
            CombinedData = new List<ActivityWrapper>();

            //combine
            foreach (var activity in activities)
            {
                CombinedData.Add(new ActivityWrapper { Type = WrappedType.HalbotActivity, HalbotActivity = activity, Date = activity.Date});
            }
            foreach (var workout in workouts)
            {
                CombinedData.Add(new ActivityWrapper { Type = WrappedType.Workout, WorkoutRecord = workout, Date = workout.Date});
            }
        }

        public string DistanceCategory(ActivityWrapper wrappedActivity)
        {
            if (wrappedActivity.Type == WrappedType.Workout)
            {
                return "workouts";
            }

            if (wrappedActivity.Type == WrappedType.HalbotActivity)
            {
                var activity = wrappedActivity.HalbotActivity;
                var category = "xshort";
                

                if (activity.Distance > 5000)
                {
                    category = "short";
                }
                if (activity.Distance > 12000)
                {
                    category = "medium";
                }
                if (activity.Distance > 22000)
                {
                    category = "long";
                }
                if (activity.Distance > 35000)
                {
                    category = "xlong";
                }

                return category;
            }

            return string.Empty;
        }
    }
}