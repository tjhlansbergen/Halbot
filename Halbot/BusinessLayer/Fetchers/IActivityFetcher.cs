using System.Collections.Generic;
using Halbot.Data.Records;

namespace Halbot.BusinessLayer.Fetchers
{
    public interface IActivityFetcher
    {
        List<ActivityRecord> CreateRecords(string activities);
    }
}