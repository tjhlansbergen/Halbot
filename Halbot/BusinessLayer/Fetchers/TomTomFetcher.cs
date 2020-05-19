using Halbot.Data.Models;
using Halbot.Data.Records;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Halbot.BusinessLayer.Fetchers
{
    public class TomTomFetcher : IActivityFetcher
    {
        public List<ActivityRecord> CreateRecords(string activityUrls)
        {
            var result = new List<ActivityRecord>();

            foreach (var activityUrl in activityUrls.Split(';').Distinct())
            {
                if(string.IsNullOrWhiteSpace(activityUrl)) continue;

                var json = new WebClient().DownloadString(activityUrl.Split('?').First());
                var activity = JsonConvert.DeserializeObject<TomTomJson>(json);

                result.Add(new ActivityRecord()
                {
                    Id = activity.Id,
                    DataType = ActivityDataType.TomTom,
                    SerializedData = json
                });
            }

            return result;
        }
    }
}
