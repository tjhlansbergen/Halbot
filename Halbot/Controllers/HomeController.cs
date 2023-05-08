using Halbot.BusinessLayer.Fetchers;
using Halbot.Data;
using Halbot.Data.Records;
using Halbot.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Halbot.BusinessLayer.Translators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;


//using JsonException = System.Text.Json.JsonException;


namespace Halbot.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _dbcontext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _dbcontext = new DatabaseContext();
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View("Index", new IndexModel(ActivityCache.Get(_dbcontext)));
        }

        public IActionResult Charts()
        {
            return View("ChartsProgression", new ChartsProgressionModel(ActivityCache.Get(_dbcontext)));
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

        public IActionResult Plan()
        {
            return View("Plan", new PlanModel(PlanCache.Get()));
        }

        public IActionResult Log()
        {
            return View("Log", new LogModel(_dbcontext.LogRecords.OrderByDescending(l => l.DateTime).Take(40).ToList()));
        }

        public IActionResult NextChart(ChartsMenuModel.ChartType chart)
        {
            return ChartMenuHelper((int)chart + 1);
        }

        public IActionResult PreviousChart(ChartsMenuModel.ChartType chart)
        {
            return ChartMenuHelper((int)chart - 1);
        }

        public IActionResult Backup()
        {
            string content = Path.Join(_webHostEnvironment.ContentRootPath, "App_Data", "Halbot.db");
            return PhysicalFile(content, "application/x-sqlite3");
        }

        private ActionResult ChartMenuHelper(int index)
        {
            var numOfCharts = Enum.GetNames(typeof(ChartsMenuModel.ChartType)).Length;

            if (index == 0) index = numOfCharts;
            if (index == numOfCharts + 1) index = 1;

            return index switch
            {
                1 => View("ChartsProgression", new ChartsProgressionModel(ActivityCache.Get(_dbcontext))),
                2 => View("ChartsMetrics", new ChartsMetricsModel(ActivityCache.Get(_dbcontext))),
                3 => View("ChartsWorkload", new ChartsWorkloadModel(ActivityCache.Get(_dbcontext))),
                4 => View("ChartsVolume", new ChartsVolumeModel(ActivityCache.Get(_dbcontext))),
                5 => View("ChartsComparison", new ChartsComparisonModel(ActivityCache.Get(_dbcontext))),
                6 => View("ChartsEddington", new ChartsEddingtonModel(ActivityCache.Get(_dbcontext))),
                _ => View("ChartsProgression", new ChartsProgressionModel(ActivityCache.Get(_dbcontext)))
            };
        }
    }
}