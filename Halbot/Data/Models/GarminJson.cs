using Newtonsoft.Json;
using System;

namespace Halbot.Data.Models
{
    public class GarminJson
    {
        [JsonProperty("activityId")]
        public long ActivityId { get; set; }

        [JsonProperty("activityUUID")]
        public ActivityUuid ActivityUuid { get; set; }

        [JsonProperty("activityName")]
        public string ActivityName { get; set; }

        [JsonProperty("userProfileId")]
        public long UserProfileId { get; set; }

        [JsonProperty("isMultiSportParent")]
        public bool IsMultiSportParent { get; set; }

        [JsonProperty("activityTypeDTO")]
        public TypeDto ActivityTypeDto { get; set; }

        [JsonProperty("eventTypeDTO")]
        public TypeDto EventTypeDto { get; set; }

        [JsonProperty("accessControlRuleDTO")]
        public AccessControlRuleDto AccessControlRuleDto { get; set; }

        [JsonProperty("timeZoneUnitDTO")]
        public TimeZoneUnitDto TimeZoneUnitDto { get; set; }

        [JsonProperty("metadataDTO")]
        public MetadataDto MetadataDto { get; set; }

        [JsonProperty("summaryDTO")]
        public SummaryDto SummaryDto { get; set; }

        [JsonProperty("locationName")]
        public string LocationName { get; set; }
    }

    public class AccessControlRuleDto
    {
        [JsonProperty("typeId")]
        public long TypeId { get; set; }

        [JsonProperty("typeKey")]
        public string TypeKey { get; set; }
    }

    public class TypeDto
    {
        [JsonProperty("typeId")]
        public long TypeId { get; set; }

        [JsonProperty("typeKey")]
        public string TypeKey { get; set; }

        [JsonProperty("parentTypeId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ParentTypeId { get; set; }

        [JsonProperty("sortOrder")]
        public long? SortOrder { get; set; }
    }

    public class ActivityUuid
    {
        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
    }

    public class MetadataDto
    {
        [JsonProperty("isOriginal")]
        public bool IsOriginal { get; set; }

        [JsonProperty("deviceApplicationInstallationId")]
        public long DeviceApplicationInstallationId { get; set; }

        [JsonProperty("agentApplicationInstallationId")]
        public object AgentApplicationInstallationId { get; set; }

        [JsonProperty("agentString")]
        public object AgentString { get; set; }

        [JsonProperty("fileFormat")]
        public FileFormat FileFormat { get; set; }

        [JsonProperty("associatedCourseId")]
        public long? AssociatedCourseId { get; set; }

        [JsonProperty("lastUpdateDate")]
        public DateTimeOffset LastUpdateDate { get; set; }

        [JsonProperty("uploadedDate")]
        public DateTimeOffset UploadedDate { get; set; }

        [JsonProperty("videoUrl")]
        public object VideoUrl { get; set; }

        [JsonProperty("hasPolyline")]
        public bool HasPolyline { get; set; }

        [JsonProperty("hasChartData")]
        public bool HasChartData { get; set; }

        [JsonProperty("hasHrTimeInZones")]
        public bool HasHrTimeInZones { get; set; }

        [JsonProperty("hasPowerTimeInZones")]
        public bool HasPowerTimeInZones { get; set; }

        [JsonProperty("userInfoDto")]
        public UserInfoDto UserInfoDto { get; set; }

        [JsonProperty("chartAvailability")]
        public ChartAvailability ChartAvailability { get; set; }

        [JsonProperty("childIds")]
        public object[] ChildIds { get; set; }

        [JsonProperty("childActivityTypes")]
        public object[] ChildActivityTypes { get; set; }

        [JsonProperty("sensors")]
        public Sensor[] Sensors { get; set; }

        [JsonProperty("activityImages")]
        public object[] ActivityImages { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("diveNumber")]
        public object DiveNumber { get; set; }

        [JsonProperty("lapCount")]
        public long LapCount { get; set; }

        [JsonProperty("associatedWorkoutId")]
        public object AssociatedWorkoutId { get; set; }

        [JsonProperty("isAtpActivity")]
        public object IsAtpActivity { get; set; }

        [JsonProperty("deviceMetaDataDTO")]
        public DeviceMetaDataDto DeviceMetaDataDto { get; set; }

        [JsonProperty("hasIntensityIntervals")]
        public bool HasIntensityIntervals { get; set; }

        [JsonProperty("hasSplits")]
        public bool HasSplits { get; set; }

        [JsonProperty("personalRecord")]
        public bool PersonalRecord { get; set; }

        [JsonProperty("elevationCorrected")]
        public bool ElevationCorrected { get; set; }

        [JsonProperty("trimmed")]
        public bool Trimmed { get; set; }

        [JsonProperty("favorite")]
        public bool Favorite { get; set; }

        [JsonProperty("manualActivity")]
        public bool ManualActivity { get; set; }

        [JsonProperty("autoCalcCalories")]
        public bool AutoCalcCalories { get; set; }

        [JsonProperty("gcj02")]
        public bool Gcj02 { get; set; }
    }

    public class ChartAvailability
    {
        [JsonProperty("showDistance")]
        public bool ShowDistance { get; set; }

        [JsonProperty("showDuration")]
        public bool ShowDuration { get; set; }

        [JsonProperty("showElevation")]
        public bool ShowElevation { get; set; }

        [JsonProperty("showHeartRate")]
        public bool ShowHeartRate { get; set; }

        [JsonProperty("showMovingDuration")]
        public bool ShowMovingDuration { get; set; }

        [JsonProperty("showMovingSpeed")]
        public bool ShowMovingSpeed { get; set; }

        [JsonProperty("showSpeed")]
        public bool ShowSpeed { get; set; }

        [JsonProperty("showTimestamp")]
        public bool ShowTimestamp { get; set; }
    }

    public class DeviceMetaDataDto
    {
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("deviceTypePk")]
        public long DeviceTypePk { get; set; }

        [JsonProperty("deviceVersionPk")]
        public long DeviceVersionPk { get; set; }
    }

    public class FileFormat
    {
        [JsonProperty("formatId")]
        public long FormatId { get; set; }

        [JsonProperty("formatKey")]
        public string FormatKey { get; set; }
    }

    public class Sensor
    {
        [JsonProperty("sku", NullValueHandling = NullValueHandling.Ignore)]
        public string Sku { get; set; }

        [JsonProperty("softwareVersion")]
        public double SoftwareVersion { get; set; }

        [JsonProperty("localDeviceType", NullValueHandling = NullValueHandling.Ignore)]
        public string LocalDeviceType { get; set; }
    }

    public class UserInfoDto
    {
        [JsonProperty("userProfilePk")]
        public long UserProfilePk { get; set; }

        [JsonProperty("displayname")]
        public Guid Displayname { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("profileImageUrlLarge")]
        public object ProfileImageUrlLarge { get; set; }

        [JsonProperty("profileImageUrlMedium")]
        public Uri ProfileImageUrlMedium { get; set; }

        [JsonProperty("profileImageUrlSmall")]
        public Uri ProfileImageUrlSmall { get; set; }

        [JsonProperty("userPro")]
        public bool UserPro { get; set; }
    }

    public class SummaryDto
    {
        [JsonProperty("startTimeLocal")]
        public DateTimeOffset StartTimeLocal { get; set; }

        [JsonProperty("startTimeGMT")]
        public DateTimeOffset StartTimeGmt { get; set; }

        [JsonProperty("startLatitude")]
        public double StartLatitude { get; set; }

        [JsonProperty("startLongitude")]
        public double StartLongitude { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("movingDuration")]
        public double MovingDuration { get; set; }

        [JsonProperty("elapsedDuration")]
        public double ElapsedDuration { get; set; }

        [JsonProperty("elevationGain")]
        public double ElevationGain { get; set; }

        [JsonProperty("elevationLoss")]
        public double ElevationLoss { get; set; }

        [JsonProperty("maxElevation")]
        public double MaxElevation { get; set; }

        [JsonProperty("minElevation")]
        public double MinElevation { get; set; }

        [JsonProperty("averageSpeed")]
        public double AverageSpeed { get; set; }

        [JsonProperty("averageMovingSpeed")]
        public double AverageMovingSpeed { get; set; }

        [JsonProperty("maxSpeed")]
        public double MaxSpeed { get; set; }

        [JsonProperty("calories")]
        public double Calories { get; set; }

        [JsonProperty("averageHR")]
        public double AverageHr { get; set; }

        [JsonProperty("maxHR")]
        public double MaxHr { get; set; }

        [JsonProperty("averageRunCadence")]
        public double AverageRunCadence { get; set; }

        [JsonProperty("maxRunCadence")]
        public double MaxRunCadence { get; set; }

        [JsonProperty("trainingEffect")]
        public double TrainingEffect { get; set; }

        [JsonProperty("anaerobicTrainingEffect")]
        public double AnaerobicTrainingEffect { get; set; }

        [JsonProperty("aerobicTrainingEffectMessage")]
        public string AerobicTrainingEffectMessage { get; set; }

        [JsonProperty("anaerobicTrainingEffectMessage")]
        public string AnaerobicTrainingEffectMessage { get; set; }

        [JsonProperty("endLatitude")]
        public double EndLatitude { get; set; }

        [JsonProperty("endLongitude")]
        public double EndLongitude { get; set; }

        [JsonProperty("maxVerticalSpeed")]
        public double MaxVerticalSpeed { get; set; }

        [JsonProperty("waterEstimated")]
        public double WaterEstimated { get; set; }

        [JsonProperty("minActivityLapDuration")]
        public double MinActivityLapDuration { get; set; }
    }

    public class TimeZoneUnitDto
    {
        [JsonProperty("unitId")]
        public long UnitId { get; set; }

        [JsonProperty("unitKey")]
        public string UnitKey { get; set; }

        [JsonProperty("factor")]
        public long Factor { get; set; }

        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }
    }
}
