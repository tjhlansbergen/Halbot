using Halbot.Data.Models;
using Halbot.Data.Records;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Halbot.BusinessLayer.Fetchers
{
    public class ClassicFetcher : IActivityFetcher
    {
        public List<ActivityRecord> CreateRecords(string activityJsons)
        {
            var result = new List<ActivityRecord>();

            foreach (var activityJson in activityJsons.Split(';'))
            {
                var activity = JsonConvert.DeserializeObject<ClassicJson>(activityJson);

                result.Add(new ActivityRecord()
                {
                    Id = activity.Id,
                    DataType = ActivityDataType.Classic,
                    SerializedData = activityJson
                });
            }

            return result;
        }
    }
}
