using Halbot.Data.Records;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Halbot.Data.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Globalization;

namespace Halbot.BusinessLayer.Fetchers
{
    public class GarminFetcher : IActivityFetcher
    {
        public List<ActivityRecord> CreateRecords(string activityIds)
        {
            var result = new List<ActivityRecord>();

            foreach (var activityId in ExtractGarminActivityId(activityIds))
            {
                //result.Add(FetchFromGarmin(activityId));
                result.Add(ScrapeFromGarmin(activityId));
            }

            return result;
        }

        // This method broke on oct 20 2023 with no resolution yet, see https://github.com/petergardfjall/garminexport/issues/106
        private static ActivityRecord FetchFromGarmin(string activityId)
        {
            if (string.IsNullOrWhiteSpace(activityId)) return null;

            var url = $"https://connect.garmin.com/proxy/activity-service/activity/{activityId}";
            string json;
            using (var client = new WebClient())
            {
                client.Headers.Add("nk", "NT");
                json = client.DownloadString(url);
            }

            var activity = JsonConvert.DeserializeObject<GarminJson>(json);

            return new ActivityRecord()
            {
                Id = activity.ActivityId,
                DataType = ActivityDataType.Garmin,
                SerializedData = json
            };
        }

        // this is a (hopefully) temporary workaround until a way is found to fetch the full data as JSON
        private static ActivityRecord ScrapeFromGarmin(string activityId)
        {
            var content = new HttpClient().GetStringAsync($"https://connect.garmin.com/modern/activity/{activityId}").Result;
            var scraped = scrapeGarminActivity(content);
            long id = long.Parse(activityId);

            var garminActivity = new GarminJson
            {
                ActivityId = id,
                ActivityName = scraped.Title,
                SummaryDto = new SummaryDto
                {
                    ElevationGain = scraped.Climb,
                    StartTimeLocal = scraped.Date,
                    Distance = scraped.DistanceMeters,
                    Duration = scraped.DurationSeconds,
                    StartLatitude = scraped.Latitude,
                    EndLatitude = scraped.Latitude,
                    StartLongitude = scraped.Longitude,
                    EndLongitude = scraped.Longitude,
                    AverageSpeed = scraped.SpeedMetersPerSecond
                }
            };


            return new ActivityRecord()
            {
                Id = id,
                DataType = ActivityDataType.Garmin,
                SerializedData = JsonConvert.SerializeObject(garminActivity),
            };
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

        private static ScrapedGarminActivity scrapeGarminActivity(string content)
        {
            var result = new ScrapedGarminActivity();
            var splits = content.Split("meta");

            result.Title = splits.Single(s => s.Contains("og:title")).Split('"')[3];
            result.Latitude = double.Parse(splits.Single(s => s.Contains("og:latitude")).Split('"')[3], CultureInfo.InvariantCulture);
            result.Longitude = double.Parse(splits.Single(s => s.Contains("og:longitude\"")).Split('"')[3], CultureInfo.InvariantCulture);

            var ogDesciption = splits.Single(s => s.Contains("og:description")).Split('"')[3];

            result.DistanceMeters = double.Parse(ogDesciption.Split('|').Single(s => s.Contains("Distance")).Split(' ')[1], CultureInfo.InvariantCulture) * 1000;
            result.Climb = double.Parse(ogDesciption.Split('|').Single(s => s.Contains("Elevation")).Split(' ')[2], CultureInfo.InvariantCulture);

            var duration = ogDesciption.Split('|').Single(s => s.Contains("Time")).Split(' ')[2].Trim();
            if (duration.Split(':').Length == 2)
            {
                duration = "00:" + duration;
            }
            result.DurationSeconds = TimeSpan.Parse(duration, CultureInfo.InvariantCulture).TotalSeconds;

            result.SpeedMetersPerSecond = result.DistanceMeters / result.DurationSeconds;

            return result;
        }

        public class ScrapedGarminActivity
        {
            public string Title { get; set; }
            public double DistanceMeters { get; set; }
            public double DurationSeconds { get; set; }
            public double Climb { get; set; }
            public double SpeedMetersPerSecond { get; set; }

            public double Latitude { get; set; }
            public double Longitude { get; set; }

            public DateTime Date { get; set; } = DateTime.UtcNow.Date.AddHours(1);  // hacky
        }
    }
}
