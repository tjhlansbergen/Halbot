using System.Collections.Generic;

namespace Halbot.Models
{
    public class IndexModel
    {
        //properties
        public List<HalbotActivity> Activities { get; private set; }

        //constructor
        public IndexModel(List<HalbotActivity> activities)
        {
            //initialize
            Activities = activities;
        }

        public string DistanceCategory(HalbotActivity activity)
        {
            var category = "xshort";

            if(activity.Distance > 5000)
            {
                category = "short";
            }
            if(activity.Distance > 12000)
            {
                category = "medium";
            }
            if(activity.Distance > 22000)
            {
                category = "long";
            }
            if(activity.Distance > 35000)
            {
                category = "xlong";
            }

            return category;
        }
    }
}