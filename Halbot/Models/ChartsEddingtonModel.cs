using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Halbot.Models
{
    public class ChartsEddingtonModel
    {
        public class EddingtonNumber
        {
            public int Number { get; set; }
            public int ActivityCount { get; set; }
            public DateTime DateCompleted { get; set; }
            public bool EddingtonComplete => ActivityCount >= Number;
            
        }

        public List<HalbotActivity> Activities { get; }
        public List<EddingtonNumber> EddingtonNumbers { get; private set; }

        public ChartsEddingtonModel(List<HalbotActivity> activities)
        {
            //initialize data
            Activities = activities;
            Fill(50);
        }

        private void Fill(int rows)
        {
            EddingtonNumbers = new List<EddingtonNumber>();

            for (int i = 1; i <= rows; i++)
            {
                EddingtonNumbers.Add(new EddingtonNumber
                {
                    Number = i,
                    ActivityCount = Activities.Count(activity => activity.Distance >= i*1000)
                });
            }

            foreach (var eddingtonNumberCompleted in EddingtonNumbers.Where(e => e.EddingtonComplete))
            {
                eddingtonNumberCompleted.DateCompleted = Activities.Where(a => a.Distance >= eddingtonNumberCompleted.Number*1000).OrderBy(a => a.Date).ElementAt(eddingtonNumberCompleted.Number).Date;
            }
        }
    }
}
