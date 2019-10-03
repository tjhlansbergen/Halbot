using Halbot.Code;
using System.Linq;
using System.Collections.Generic;
using System;

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