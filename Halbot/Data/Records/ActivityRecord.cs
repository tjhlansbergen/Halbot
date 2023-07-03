
using System.ComponentModel.DataAnnotations.Schema;

namespace Halbot.Data.Records
{
    public class ActivityRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; } // primary key
        public ActivityDataType DataType { get; set; }
        public string SerializedData { get; set; }
        public string Description { get; set; }
        public bool IsRace { get; set; }
        public string Gpx { get; set; }     // re-used as 'journal'
    }
}
