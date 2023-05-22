using Halbot.Data.Records;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Halbot.Data.Models;
using Newtonsoft.Json;

namespace Halbot.BusinessLayer.Fetchers
{
    public class GarminFetcher : IActivityFetcher
    {
        public List<ActivityRecord> CreateRecords(string activityIds)
        {
            var result = new List<ActivityRecord>();

            foreach (var activityId in ExtractGarminActivityId(activityIds))
            {
                if (string.IsNullOrWhiteSpace(activityId)) continue;

                var url = $"https://connect.garmin.com/proxy/activity-service/activity/{activityId}";
                string json;
                using (var client = new WebClient())
                {
                    client.Headers.Add("nk", "NT");
                    json = client.DownloadString(url);
                }

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

        private static IEnumerable<string> ExtractGarminActivityId(string input)
        {
            var result = new List<string>();

            if (string.IsNullOrEmpty(input)) return result;

            List<string> candidates = new List<string> { };

            candidates.AddRange(input.Split('/'));
            candidates.AddRange(input.Split('\\'));
            candidates.AddRange(input.Split(';'));
            candidates.AddRange(input.Split(','));

            foreach (var candidate in candidates)
            {
                if (candidate.All(char.IsDigit) && candidate.Length > 8 && candidate.Length < 12) result.Add(candidate);
            }

            return result.Distinct();
        }
    }
}
