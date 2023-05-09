using System;
using Halbot.Data.Records;
using System.Collections.Generic;
using System.Linq;

namespace Halbot.Models
{
    public class PlanModel
    {
        // properties
        public Dictionary<int, IEnumerable<PlanRecord>> Weeks { get; }

        // constructor
        public PlanModel(List<PlanRecord> planRecords)
        {
            //initialize (our cache holds all plan records, here we select those we'd like to show 
            var currentWeekNumber = DateTime.UtcNow.Week();
            Weeks = Enumerable.Range(currentWeekNumber, 5)
                .ToDictionary(i => i, i => planRecords.Where(r => r.Date.Week() == i))
                .Where(p => p.Value.Any())
                .ToDictionary(p => p.Key, p => p.Value);
        }
    }
}
