using System.Collections.Generic;

namespace Halbot.Models
{
    public class RacesModel
    {
        //properties
        public List<HalbotActivity> Races { get; private set; }

        //constructor
        public RacesModel(List<HalbotActivity> races)
        {
            //initialize
            Races = races;
        }
    }
}
