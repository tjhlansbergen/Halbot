using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Halbot.Models
{
    public class ChartsMenuModel
    {
        public enum ChartType { Progression, Workload }

        public ChartType CurrentChart { get; set; }
    }
}
