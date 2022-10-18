using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Halbot.BusinessLayer.Translators;
using Halbot.Models;

namespace Halbot.Data
{
    public static class ActivityCache
    {
        private static List<HalbotActivity> _activities = new List<HalbotActivity>();

        public static List<HalbotActivity> Get(DatabaseContext context)
        {
            if (_activities.Count != context.ActivityRecords.Count())
            {
                _activities = new MasterTranslator().Parse(context.ActivityRecords);
            }

            return _activities;
        }

        public static void InvalidateCache()
        {
            _activities.Clear();
        }
    }
}
