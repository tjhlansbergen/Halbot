using System;

namespace Halbot.Models
{
    public class ChartsMenuModel
    {
        public enum ChartType { Progression, Workload, Volume, Comparison }

        public ChartType CurrentChart { get; set; }
    }
}
