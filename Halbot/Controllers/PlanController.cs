using System;
using System.Linq;
using Halbot.Data;
using Halbot.Data.Records;
using Microsoft.AspNetCore.Mvc;

namespace Halbot.Controllers
{
    public class PlanController : Controller
    {
        private readonly DatabaseContext _dbcontext = new DatabaseContext();
        private readonly Logger _logger = new Logger();

        public IActionResult Add(DateTime date, string description, string color)
        {
            var record = new PlanRecord
            {
                Date = date,
                Description = description,
                Color = (color == "#000000") ? "#ffffff" : color    // turn black into white
            };

            _dbcontext.PlanRecords.Add(record);
            _dbcontext.SaveChanges();

            return RedirectToAction("Plan", "Home");
        }

        public IActionResult Delete(long id)
        {
            try
            {
                _dbcontext.PlanRecords.Remove(_dbcontext.PlanRecords.Single(r => r.Id == id));
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log(LogSeverityLevel.Error, $"Error deleting plan record with ID: {id}");
                _logger.Log(LogSeverityLevel.Error, ex.Message);
                return RedirectToAction("Log", "Home");
            }

            return RedirectToAction("Plan", "Home");
        }

        public IActionResult ToggleComplete(long id)
        {
            try
            {
                _dbcontext.PlanRecords.Single(r => r.Id == id).Completed = !_dbcontext.PlanRecords.Single(r => r.Id == id).Completed;
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log(LogSeverityLevel.Error, $"Error (un)completing plan record with ID: {id}");
                _logger.Log(LogSeverityLevel.Error, ex.Message);
                return RedirectToAction("Log", "Home");
            }

            return RedirectToAction("Plan", "Home");
        }
    }
}
