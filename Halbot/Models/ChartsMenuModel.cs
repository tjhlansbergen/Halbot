using System;

namespace Halbot.Models
{
    public class ChartsMenuModel
    {
        public enum ChartType { Progression = 1, Workload = 2, Volume = 3, Comparison = 4, Eddington = 5 }

        public ChartType CurrentChart { get; set; }
    }
}
