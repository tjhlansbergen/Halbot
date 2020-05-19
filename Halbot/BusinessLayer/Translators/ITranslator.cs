using System.Collections.Generic;
using Halbot.Data.Records;
using Halbot.Models;

namespace Halbot.BusinessLayer.Translators
{
    public interface ITranslator
    {
        List<HalbotActivity> Parse(IEnumerable<ActivityRecord> records);
    }
}