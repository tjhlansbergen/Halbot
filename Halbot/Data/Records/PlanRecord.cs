using System;

namespace Halbot.Data.Records
{
    public class PlanRecord
    {
        public long Id { get; set; } // primary key
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public bool Completed { get; set; }
    }
}
