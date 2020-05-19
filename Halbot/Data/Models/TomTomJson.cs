using Newtonsoft.Json;
using System;

namespace Halbot.Data.Models
{
    public class TomTomJson
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("activity_type_id")]
        public long ActivityTypeId { get; set; }

        [JsonProperty("start_datetime")]
        public DateTimeOffset StartDatetime { get; set; }

        [JsonProperty("start_datetime_user")]
        public DateTimeOffset StartDatetimeUser { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("activity_type_id_tt")]
        public long ActivityTypeIdTt { get; set; }

        [JsonProperty("display_offset_seconds")]
        public long DisplayOffsetSeconds { get; set; }

        [JsonProperty("links")]
        public Links Links { get; set; }

        [JsonProperty("formats")]
        public string[] Formats { get; set; }

        [JsonProperty("zones")]
        public long[] Zones { get; set; }

        [JsonProperty("bounding_box")]
        public BoundingBox BoundingBox { get; set; }

        [JsonProperty("aggregates")]
        public Aggregates Aggregates { get; set; }
    }

    public class Aggregates
    {
        [JsonProperty("active_time_total")]
        public double ActiveTimeTotal { get; set; }

        [JsonProperty("distance_total")]
        public double DistanceTotal { get; set; }

        [JsonProperty("steps_total")]
        public long StepsTotal { get; set; }

        [JsonProperty("elapsed_time_total")]
        public long ElapsedTimeTotal { get; set; }

        [JsonProperty("metabolic_energy_total")]
        public long MetabolicEnergyTotal { get; set; }

        [JsonProperty("speed_avg")]
        public double SpeedAvg { get; set; }

        [JsonProperty("climb_total")]
        public double ClimbTotal { get; set; }

        [JsonProperty("descent_total")]
        public double DescentTotal { get; set; }

        [JsonProperty("heartrate_avg")]
        public double HeartrateAvg { get; set; }

        [JsonProperty("hrz_dist")]
        public long[] HrzDist { get; set; }

        [JsonProperty("hrz_none")]
        public long HrzNone { get; set; }

        [JsonProperty("moving_time_total")]
        public long MovingTimeTotal { get; set; }

        [JsonProperty("moving_speed_avg")]
        public double MovingSpeedAvg { get; set; }
    }

    public partial class BoundingBox
    {
        [JsonProperty("north_east")]
        public NorthEast NorthEast { get; set; }

        [JsonProperty("south_west")]
        public NorthEast SouthWest { get; set; }
    }

    public partial class NorthEast
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("image")]
        public Uri Image { get; set; }

        [JsonProperty("webview")]
        public string Webview { get; set; }

        [JsonProperty("convert_to_trail")]
        public Uri ConvertToTrail { get; set; }

        [JsonProperty("self")]
        public Uri Self { get; set; }
    }

    public partial class User
    {
        [JsonProperty("devices")]
        public object[] Devices { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("traits")]
        public object[] Traits { get; set; }

        [JsonProperty("user_prefs")]
        public UserPrefs UserPrefs { get; set; }
    }

    public class UserPrefs
    {
        [JsonProperty("default")]
        public Default Default { get; set; }

        [JsonProperty("overrides")]
        public Overrides Overrides { get; set; }

        [JsonProperty("clock")]
        public string Clock { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("wrist")]
        public string Wrist { get; set; }

        [JsonProperty("auto_pauses_enabled")]
        public AutoPausesEnabled AutoPausesEnabled { get; set; }
    }

    public class AutoPausesEnabled
    {
        [JsonProperty("RUN")]
        public bool Run { get; set; }

        [JsonProperty("RUN_TRAIL")]
        public bool RunTrail { get; set; }

        [JsonProperty("CYCLE")]
        public bool Cycle { get; set; }

        [JsonProperty("WALK_HIKE")]
        public bool WalkHike { get; set; }

        [JsonProperty("SKI")]
        public bool Ski { get; set; }

        [JsonProperty("SNOWBOARD")]
        public bool Snowboard { get; set; }

        [JsonProperty("FREESTYLE")]
        public bool Freestyle { get; set; }
    }

    public class Default
    {
        [JsonProperty("distance")]
        public string Distance { get; set; }

        [JsonProperty("energy")]
        public string Energy { get; set; }
    }

    public partial class Overrides
    {
    }
}
