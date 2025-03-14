﻿using Halbot.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Halbot.Data.Models;
using Newtonsoft.Json;

namespace Halbot.Controllers
{
    public class ActivityController : Controller
    {
        private readonly DatabaseContext _dbcontext = new DatabaseContext();
        private readonly Logger _logger = new Logger();

        public IActionResult Delete(long id)
        {
            try
            {
                _dbcontext.ActivityRecords.Remove(_dbcontext.ActivityRecords.Single(r => r.Id == id));
                _dbcontext.SaveChanges();

                ActivityCache.InvalidateCache();

                _logger.Log(LogSeverityLevel.Info, $"Deleted activity with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogSeverityLevel.Error, $"Error deleting activity with ID: {id}");
                _logger.Log(LogSeverityLevel.Error, ex.Message);
                return RedirectToAction("Log", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ToggleRace(long id)
        {
            try
            {
                var record = _dbcontext.ActivityRecords.Single(r => r.Id == id);
                record.IsRace = !record.IsRace;
                _dbcontext.SaveChanges();

                ActivityCache.InvalidateCache();

                _logger.Log(LogSeverityLevel.Info, $"Toggled IsRace for activity with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogSeverityLevel.Error, $"Error toggling IsRace for activity with ID: {id}");
                _logger.Log(LogSeverityLevel.Error, ex.Message);
                return RedirectToAction("Log", "Home");
            }

            return RedirectToAction("Run", "Home", new { id = id });
        }

        public IActionResult Save(string description, string journal, int heartrate, double maxElevation, DateTime date, long id)
        {
            try
            {
                var record = _dbcontext.ActivityRecords.Single(r => r.Id == id);
                record.Description = description;
                record.Gpx = journal;

                if (record.DataType == ActivityDataType.Garmin)
                {
                    var activity = JsonConvert.DeserializeObject<GarminJson>(record.SerializedData);
                    activity.SummaryDto.AverageHr = heartrate;
                    activity.SummaryDto.MaxElevation = maxElevation;
                    activity.SummaryDto.StartTimeLocal = date;
                    record.SerializedData = JsonConvert.SerializeObject(activity);
                }

                _dbcontext.SaveChanges();

                ActivityCache.InvalidateCache();

                _logger.Log(LogSeverityLevel.Info, $"Changed description for activity with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogSeverityLevel.Error, $"Error changing description for activity with ID: {id}");
                _logger.Log(LogSeverityLevel.Error, ex.Message);
                return RedirectToAction("Log", "Home");
            }

            return RedirectToAction("Run", "Home", new { id });
        }

        public IActionResult EditDescription(long id)
        {
            return View("EditRun", ActivityCache.Get(_dbcontext).Single(a => a.Id == id));
        }
    }
}