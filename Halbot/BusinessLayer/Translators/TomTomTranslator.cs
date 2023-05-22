using System.Collections.Generic;
using Halbot.Data.Models;
using Halbot.Data.Records;
using Halbot.Models;
using Newtonsoft.Json;

namespace Halbot.BusinessLayer.Translators
{
    public class TomTomTranslator : ITranslator
    {
        public List<HalbotActivity> Parse(IEnumerable<ActivityRecord> records)
        {
            var result = new List<HalbotActivity>();

            foreach (var record in records)
            {
                // invalid data type, return empty object
                if (record.DataType != ActivityDataType.TomTom)
                {
                    result.Add(new HalbotActivity() {Id = record.Id});
                    continue;
                }

                var tomTomActivity = JsonConvert.DeserializeObject<TomTomJson>(record.SerializedData);
                var halbotActivity = new HalbotActivity();

                halbotActivity.Id = record.Id;
                halbotActivity.Description = record.Description;
                halbotActivity.IsRace = record.IsRace;
                halbotActivity.DataType = record.DataType;

                halbotActivity.Climb = tomTomActivity.Aggregates.ClimbTotal;
                halbotActivity.Descent = tomTomActivity.Aggregates.DescentTotal;
                halbotActivity.Date = tomTomActivity.StartDatetimeUser.DateTime;
                halbotActivity.Distance = tomTomActivity.Aggregates.DistanceTotal;
                halbotActivity.Duration = tomTomActivity.Aggregates.ActiveTimeTotal;
                halbotActivity.Heartrate = tomTomActivity.Aggregates.HeartrateAvg;
                halbotActivity.Lat = (tomTomActivity.BoundingBox.NorthEast.Lat + tomTomActivity.BoundingBox.SouthWest.Lat) / 2;
                halbotActivity.Lng = (tomTomActivity.BoundingBox.NorthEast.Lng + tomTomActivity.BoundingBox.SouthWest.Lng) / 2;
                halbotActivity.Speed = tomTomActivity.Aggregates.SpeedAvg;
                halbotActivity.Url = tomTomActivity.Links.Self;

                result.Add(halbotActivity);
            }

            return result;
        }
    }
}
