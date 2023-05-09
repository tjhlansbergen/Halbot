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

        public static List<PlanRecord> Get()
        {
            if (!_plannedActivities.Any())
            {
                FetchFromTrello();
            }

            return _plannedActivities;
        }

        public static void InvalidateCache()
        {
            _plannedActivities.Clear();
        }

        private static void FetchFromTrello()
        {
            const string url = "https://api.trello.com/1/boards/fvn5LZrM/cards?key=c0f37d8292dbac2cb721710266ba4f61&token=ATTAec0056728891065984b110fa3c376975aa79a39c7e2137eaf000e5332a26650328695366\r\n";
            string json;
            using (var client = new WebClient())
            {
                json = client.DownloadString(url);
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
