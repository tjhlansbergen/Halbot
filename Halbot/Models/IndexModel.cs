using System.Collections.Generic;
using Halbot.Code;

namespace Halbot.Models
{
    public class IndexModel
    {
        //properties
        public List<HalbotActivity> Activities { get; private set; }

        //constructor
        public IndexModel(List<HalbotActivity> dbactivities)
        {
            //initialize
            Activities = dbactivities;
        }
    }
}