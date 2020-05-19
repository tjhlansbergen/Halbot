using Halbot.Data.Records;
using Halbot.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Halbot.Data.Models;
using Newtonsoft.Json;

namespace Halbot.BusinessLayer.Translators
{
    public class ClassicTranslator : ITranslator
    {
        public List<HalbotActivity> Parse(IEnumerable<ActivityRecord> records)
        {
            var result = new List<HalbotActivity>();

            foreach (var record in records)
            {
                // invalid data type, return empty object
                if (record.DataType != ActivityDataType.Classic)
                {
                    result.Add(new HalbotActivity() { Id = record.Id });
                    continue;
                }

                // valid data type, start parsing
                var classicActivity = JsonConvert.DeserializeObject<ClassicJson>(record.SerializedData);
                var halbotActivity = new HalbotActivity();

                halbotActivity.Id = record.Id;
                halbotActivity.Description = record.Description;
                halbotActivity.IsRace = record.IsRace;
                halbotActivity.DataType = record.DataType;

                halbotActivity.Date = classicActivity.StartDatetime.DateTime;
                halbotActivity.Distance = double.TryParse(classicActivity.DistanceTotal, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out double distanceValue) ? distanceValue : 0;
                halbotActivity.Speed = double.TryParse(classicActivity.SpeedAvg, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out double speedValue) ? speedValue : 0;

                result.Add(halbotActivity);
            }

            return result;
        }
    }
}
