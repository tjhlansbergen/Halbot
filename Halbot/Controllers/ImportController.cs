using Halbot.BusinessLayer.Fetchers;
using Halbot.Data;
using Halbot.Data.Records;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Halbot.Controllers
{
    public class ImportController : Controller
    {
        private readonly DatabaseContext _dbcontext = new DatabaseContext();
        private readonly Logger _logger = new Logger();

        public IActionResult LoadClassicActivities(string classicActivities)
        {
            if (string.IsNullOrEmpty(classicActivities))
            {
                _logger.Log(LogSeverityLevel.Warning, $"Added NO classic activities to database because input string was empty");
                return RedirectToAction("Log", "Home");
            }

            try
            {
                var records = new ClassicFetcher().CreateRecords(classicActivities);

                _logger.Log(LogSeverityLevel.Info, $"Adding {records.Count} classic activities to the database");
                ActivityImport(_dbcontext, _logger, records);
            }
            catch (Exception ex)
            {
                _logger.Log(LogSeverityLevel.Error, $"Import exception");
                _logger.Log(LogSeverityLevel.Error, ex.Message);
                return RedirectToAction("Log", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult LoadTomTomActivities(string tomTomActivityUrls)
        {
            if (string.IsNullOrEmpty(tomTomActivityUrls))
            {
                _logger.Log(LogSeverityLevel.Warning, $"Added NO TomTom activities to database because input string was empty");
                return RedirectToAction("Log", "Home");
            }

            try
            {
                var records = new TomTomFetcher().CreateRecords(tomTomActivityUrls);

                _logger.Log(LogSeverityLevel.Info, $"Adding {records.Count} TomTom activities to the database");
                ActivityImport(_dbcontext, _logger, records);
            }
            catch (Exception ex)
            {
                _logger.Log(LogSeverityLevel.Error, $"Import exception");
                _logger.Log(LogSeverityLevel.Error, ex.Message);
                return RedirectToAction("Log", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult LoadGarminActivities(string garminActivityIds)
        {
            if (string.IsNullOrEmpty(garminActivityIds))
            {
                _logger.Log(LogSeverityLevel.Warning, $"Added NO Garmin activities to database because input string was empty");
                return RedirectToAction("Log", "Home");
            }

            try
            {
                var records = new GarminFetcher().CreateRecords(garminActivityIds);
                ActivityImport(_dbcontext, _logger, records);
            }
            catch (Exception ex)
            {
                _logger.Log(LogSeverityLevel.Error, $"Import exception");
                _logger.Log(LogSeverityLevel.Error, ex.Message);
                return RedirectToAction("Log", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public static void ActivityImport(DatabaseContext context, Logger logger, List<ActivityRecord> records, bool verbose = true)
        {
            var count = 0;
            foreach (var incomingRecord in records)
            {
                // check if we already have the activity by ID
                if (context.ActivityRecords.Any(r => r.Id == incomingRecord.Id))
                {
                    if (verbose)
                    {
                        logger.Log(LogSeverityLevel.Warning, $"Activity with ID {incomingRecord.Id} already exists!");
                    }
                }
                else
                {
                    context.ActivityRecords.Add(incomingRecord);
                    count++;
                }
            }
            context.SaveChanges();

            if (verbose)
            {
                logger.Log(LogSeverityLevel.Info, $"Added {records.Count} Garmin activities to the database");
            }
        }
    }
}