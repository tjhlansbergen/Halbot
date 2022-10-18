using Halbot.Data.Models;
using Halbot.Data.Records;
using Halbot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Halbot.BusinessLayer.Translators
{
    public class GarminTranslator : ITranslator
    {
        public List<HalbotActivity> Parse(IEnumerable<ActivityRecord> records)
        {
            var result = new List<HalbotActivity>();

            foreach (var record in records)
            {
                switch (record.DataType)
                {

                    case ActivityDataType.Garmin:
                        result.Add(MapGarmin(record));
                        break;
                    case ActivityDataType.FlatGarmin:
                        result.Add(MapFlatGarmin(record));
                        break;
                    default:
                        // invalid data type, return empty object
                        result.Add(new HalbotActivity() { Id = record.Id });
                        break;
                }
            }

            return result;
        }

        private static HalbotActivity MapGarmin(ActivityRecord record)
        {
            var garminActivity = JsonConvert.DeserializeObject<GarminJson>(record.SerializedData);
            var halbotActivity = new HalbotActivity();

            halbotActivity.Id = record.Id;
            halbotActivity.Description = record.Description;
            halbotActivity.IsRace = record.IsRace;
            halbotActivity.DataType = record.DataType;

            halbotActivity.Climb = garminActivity.SummaryDto.ElevationGain;
            halbotActivity.Descent = garminActivity.SummaryDto.ElevationLoss;
            halbotActivity.Date = garminActivity.SummaryDto.StartTimeLocal.DateTime;
            halbotActivity.Distance = garminActivity.SummaryDto.Distance;
            halbotActivity.Duration = garminActivity.SummaryDto.Duration;
            halbotActivity.Heartrate = garminActivity.SummaryDto.AverageHr;
            halbotActivity.Lat = (garminActivity.SummaryDto.StartLatitude + garminActivity.SummaryDto.EndLatitude) / 2;
            halbotActivity.Lng = (garminActivity.SummaryDto.StartLongitude + garminActivity.SummaryDto.EndLongitude) / 2;
            halbotActivity.Speed = garminActivity.SummaryDto.AverageSpeed;
            halbotActivity.Cadence = garminActivity.SummaryDto.AverageRunCadence;
            halbotActivity.TrainingEffect = garminActivity.SummaryDto.TrainingEffect;
            halbotActivity.AnaerobicTrainingEffect = garminActivity.SummaryDto.AnaerobicTrainingEffect;
            halbotActivity.Url = new Uri($"https://connect.garmin.com/modern/activity/{garminActivity.ActivityId}");

            return halbotActivity;
        }

        private static HalbotActivity MapFlatGarmin(ActivityRecord record)
        {
            var garminActivity = JsonConvert.DeserializeObject<FlatGarminJson>(record.SerializedData);
            var halbotActivity = new HalbotActivity();

            halbotActivity.Id = record.Id;
            halbotActivity.Description = record.Description;
            halbotActivity.IsRace = record.IsRace;
            halbotActivity.DataType = record.DataType;

            halbotActivity.Climb = Math.Round(garminActivity.ElevationGain ?? 0, 2);
            halbotActivity.Descent = Math.Round(garminActivity.ElevationLoss ?? 0, 2);
            halbotActivity.Date = garminActivity.StartTimeLocal;
            halbotActivity.Distance = garminActivity.Distance ?? 0;
            halbotActivity.Duration = garminActivity.Duration ?? 0;
            halbotActivity.Heartrate = garminActivity.AverageHr ?? 0;
            halbotActivity.Lat = (garminActivity.StartLatitude ?? 0 + garminActivity.EndLatitude ?? 0) / 2;
            halbotActivity.Lng = (garminActivity.StartLongitude ?? 0 + garminActivity.EndLongitude ?? 0) / 2;
            halbotActivity.Speed = garminActivity.AverageSpeed ?? 0;
            halbotActivity.Cadence = garminActivity.AverageRunningCadenceInStepsPerMinute ?? 0;
            halbotActivity.TrainingEffect = garminActivity.AerobicTrainingEffect ?? 0;
            halbotActivity.AnaerobicTrainingEffect = garminActivity.AnaerobicTrainingEffect ?? 0;
            halbotActivity.Url = new Uri($"https://connect.garmin.com/modern/activity/{garminActivity.ActivityId}");

            return halbotActivity;
        }
    }
}
