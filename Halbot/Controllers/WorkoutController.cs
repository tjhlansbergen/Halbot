using Halbot.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Halbot.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly DatabaseContext _dbcontext = new DatabaseContext();
        private readonly Logger _logger = new Logger();

        public IActionResult Workout(long id)
        {
            return View("EditWorkout", WorkoutCache.Get(_dbcontext).Single(a => a.Id == id));
        }

        public IActionResult Save(string notes, int minutes, DateTime date, long id)
        {
            try
            {
                var record = _dbcontext.WorkoutRecords.Single(r => r.Id == id);
                record.Notes = notes;
                record.Minutes = minutes;
                record.Date = date;

                _dbcontext.SaveChanges();

                WorkoutCache.InvalidateCache();

                _logger.Log(LogSeverityLevel.Info, $"Edited workout with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogSeverityLevel.Error, $"Error editing workout with ID: {id}");
                _logger.Log(LogSeverityLevel.Error, ex.Message);
                return RedirectToAction("Log", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(long id)
        {
            try
            {
                _dbcontext.WorkoutRecords.Remove(_dbcontext.WorkoutRecords.Single(r => r.Id == id));
                _dbcontext.SaveChanges();

                WorkoutCache.InvalidateCache();

                _logger.Log(LogSeverityLevel.Info, $"Deleted workout with ID: {id}");
            }
            catch (Exception ex)
            {
                _logger.Log(LogSeverityLevel.Error, $"Error deleting workout with ID: {id}");
                _logger.Log(LogSeverityLevel.Error, ex.Message);
                return RedirectToAction("Log", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
