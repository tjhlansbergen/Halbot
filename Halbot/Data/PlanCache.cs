using System;
using Halbot.Data.Models;
using Halbot.Data.Records;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Halbot.Data
{
    public static class PlanCache
    {
        private static List<PlanRecord> _plannedActivities = new List<PlanRecord>();

        public static List<PlanRecord> Get(string trelloUrl)
        {
            if (!_plannedActivities.Any())
            {
                FetchFromTrello(trelloUrl);
            }

            return _plannedActivities;
        }

        public static void InvalidateCache()
        {
            _plannedActivities.Clear();
        }

        private static void FetchFromTrello(string trelloUrl)
        {
            string json;
            using (var client = new WebClient())
            {
                json = client.DownloadString(trelloUrl);
            }

            var cards = JsonConvert.DeserializeObject<List<TrelloCard>>(json);

            _plannedActivities = cards.Where(c => c.Name != "---")
                .Select(c => new PlanRecord
                {
                    Description = c.Name,
                    Date = c.Due?.Date ?? DateTime.MinValue,
                    Label = c.Labels.FirstOrDefault()?.Name ?? string.Empty,
                }).ToList();
        }
    }
}
