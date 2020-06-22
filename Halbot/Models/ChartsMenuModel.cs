using System;

namespace Halbot.Models
{
    public class ChartsMenuModel
    {
        public enum ChartType { Progression, Workload, Volume }

        public ChartType CurrentChart { get; set; }
    }
}
