using Halbot.Data.Records;
using Halbot.Models;
using System.Collections.Generic;
using System.Linq;

namespace Halbot.BusinessLayer.Translators
{
    public class MasterTranslator : ITranslator
    {
        public List<HalbotActivity> Parse(IEnumerable<ActivityRecord> records)
        {
            var result = new List<HalbotActivity>();

            result.AddRange(new ClassicTranslator().Parse(records.Where(r => r.DataType == ActivityDataType.Classic)));
            result.AddRange(new TomTomTranslator().Parse(records.Where(r => r.DataType == ActivityDataType.TomTom)));
            result.AddRange(new GarminTranslator().Parse(records.Where(r => r.DataType == ActivityDataType.Garmin || r.DataType == ActivityDataType.FlatGarmin)));

            return result;
        }
    }
}
