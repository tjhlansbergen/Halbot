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
    }
}