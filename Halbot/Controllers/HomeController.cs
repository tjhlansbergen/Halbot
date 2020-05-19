using Halbot.BusinessLayer.Fetchers;
using Halbot.Data;
using Halbot.Data.Records;
using Halbot.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Halbot.BusinessLayer.Translators;

//using JsonException = System.Text.Json.JsonException;


namespace Halbot.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _dbcontext = new DatabaseContext();

        public IActionResult Index()
        {
            return View("Index", new IndexModel(ActivityCache.Get(_dbcontext)));
        }

        public IActionResult Charts()
        {
            return View("Charts", new ChartsModel(ActivityCache.Get(_dbcontext)));
        }

        public IActionResult Stats()
        {
            return View("Stats", new StatsModel(ActivityCache.Get(_dbcontext)));
        }

        public IActionResult Races()
        {
            return View("Races", new RacesModel(ActivityCache.Get(_dbcontext).Where(a => a.IsRace).ToList()));
        }

        public IActionResult Map()
        {
            return View("Map", new MapModel(ActivityCache.Get(_dbcontext)));
        }

        public IActionResult Import()
        {
            return View("Import", new ImportModel());
        }

        public IActionResult Run(long id)
        {
            return View("Run", ActivityCache.Get(_dbcontext).Single(a => a.Id == id));
        }

        public IActionResult Log()
        {
            return View("Log", new LogModel(_dbcontext.LogRecords.OrderByDescending(l => l.DateTime).Take(40).ToList()));
        }
    }
}