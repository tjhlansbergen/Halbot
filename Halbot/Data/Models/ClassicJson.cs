using System;
using Newtonsoft.Json;

namespace Halbot.Data.Models
{
    public class ClassicJson
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("activity_type_id")]
        public long ActivityTypeId { get; set; }

        [JsonProperty("start_datetime")]
        public DateTimeOffset StartDatetime { get; set; }

        [JsonProperty("start_datetime_user")]
        public DateTimeOffset StartDatetimeUser { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("active_time_total")]
        public string ActiveTimeTotal { get; set; }

        [JsonProperty("distance_total")]
        public string DistanceTotal { get; set; }

        [JsonProperty("steps_total")]
        public string StepsTotal { get; set; }

        [JsonProperty("elapsed_time_total")]
        public string ElapsedTimeTotal { get; set; }

        [JsonProperty("metabolic_energy_total")]
        public string MetabolicEnergyTotal { get; set; }

        [JsonProperty("speed_avg")]
        public string SpeedAvg { get; set; }

        [JsonProperty("climb_total")]
        public string ClimbTotal { get; set; }

        [JsonProperty("descent_total")]
        public string DescentTotal { get; set; }

        [JsonProperty("heartrate_avg")]
        public string HeartrateAvg { get; set; }

        [JsonProperty("hrz_dist")]
        public string HrzDist { get; set; }

        [JsonProperty("hrz_none")]
        public long HrzNone { get; set; }

        [JsonProperty("moving_time_total")]
        public string MovingTimeTotal { get; set; }

        [JsonProperty("moving_speed_avg")]
        public string MovingSpeedAvg { get; set; }
    }
}
