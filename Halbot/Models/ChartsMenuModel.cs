using System;

namespace Halbot.Models
{
    public class ChartsMenuModel
    {
        public enum ChartType { Progression = 1, Metrics = 2, Workload = 3, Volume = 4, Comparison = 5, Eddington = 6 }

        public ChartType CurrentChart { get; set; }
    }
}
