using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Halbot.Code;
using Halbot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Halbot.Controllers
{
    public class HomeController : Controller
    {
        private readonly HalbotDBContext _dbcontext = new HalbotDBContext();

        public IActionResult Index()
        {
            return View("Index", new IndexModel(_dbcontext.DBActivities.ToList()));
        }

        public IActionResult Progress()
        {
            return View("Progress", new ProgressModel(_dbcontext.DBActivities.ToList()));
        }

        public IActionResult Edit(int id)
        {
            return View("Edit", _dbcontext.DBActivities.Where(a => a.ID == id).FirstOrDefault());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(HalbotActivity activity)
        {
            // update DB context
            //_dbcontext.DBActivities.Update(activity);

            _dbcontext.DBActivities.Attach(activity);
            _dbcontext.Entry(activity).Property(p => p.Label).IsModified = true;
            _dbcontext.Entry(activity).Property(p => p.Description).IsModified = true;

            // persist changes in DB
            _dbcontext.SaveChanges();

            // return same item
            return RedirectToAction("Edit", activity.ID);
        }
    }
}
