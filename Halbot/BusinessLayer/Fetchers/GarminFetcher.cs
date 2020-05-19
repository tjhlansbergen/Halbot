using Halbot.Data.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Halbot.Data.Models;
using Newtonsoft.Json;

namespace Halbot.BusinessLayer.Fetchers
{
    public class GarminFetcher : IActivityFetcher
    {
        public List<ActivityRecord> CreateRecords(string activityIds)
        {
            var result = new List<ActivityRecord>();

            foreach (var activityId in activityIds.Split(';').Distinct())
            {
                if (string.IsNullOrWhiteSpace(activityId)) continue;

                var url = $"https://connect.garmin.com/modern/proxy/activity-service/activity/{activityId}";
                var json = new WebClient().DownloadString(url);
                var activity = JsonConvert.DeserializeObject<GarminJson>(json);

                result.Add(new ActivityRecord()
                {
                    Id = activity.ActivityId,
                    DataType = ActivityDataType.Garmin,
                    SerializedData = json
                });
                
            }

            return result;
        }
    }
}
