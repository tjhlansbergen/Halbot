using System;
using System.Collections.Generic;
using Halbot.Data.Models;
using Halbot.Data.Records;
using Halbot.Models;
using Newtonsoft.Json;

namespace Halbot.BusinessLayer.Translators
{
    public class GarminTranslator : ITranslator
    {
        public List<HalbotActivity> Parse(IEnumerable<ActivityRecord> records)
        {
            var result = new List<HalbotActivity>();

            foreach (var record in records)
            {
                // invalid data type, return empty object
                if (record.DataType != ActivityDataType.Garmin)
                {
                    result.Add(new HalbotActivity() {Id = record.Id});
                    continue;
                }

                var garminActivity = JsonConvert.DeserializeObject<GarminJson>(record.SerializedData);
                var halbotActivity = new HalbotActivity();

                halbotActivity.Id = record.Id;
                halbotActivity.Description = record.Description;
                halbotActivity.IsRace = record.IsRace;
                halbotActivity.DataType = record.DataType;
                halbotActivity.Journal = record.Gpx;    // yes, this is a hack

                halbotActivity.Climb = garminActivity.SummaryDto.ElevationGain;
                halbotActivity.Descent = garminActivity.SummaryDto.ElevationLoss;
                halbotActivity.MaxElevation = garminActivity.SummaryDto.MaxElevation;
                halbotActivity.MinElevation = garminActivity.SummaryDto.MinElevation;
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

                result.Add(halbotActivity);
            }

            return result;
        }
    }
}
