using Halbot.Data.Records;
using System.Collections.Generic;

namespace Halbot.Models
{
    public class PlanModel
    {
        // properties
        public List<PlanRecord> PlanRecords { get; }

        // constructor
        public PlanModel(List<PlanRecord> planRecords)
        {
            //initialize
            PlanRecords = planRecords;
        }
    }
}
