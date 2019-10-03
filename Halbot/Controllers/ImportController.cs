using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Halbot.Models;
using Halbot.Code;
using System.Net;
using Newtonsoft.Json;

namespace Halbot.Controllers
{
    public class ImportController : Controller
    {
        private readonly HalbotDBContext _dbcontext = new HalbotDBContext();

        public IActionResult Import()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Import(ImportModel model)
        {
            //TODO null-checking

            foreach (string url in model.ImportURLs.Split(';'))
            {
                string parsed_url = string.Empty, json = string.Empty;

                // tomtom message format
                if (url.Contains("https:") && url.Contains("?format"))
                {
                    //parse messsage
                    int start = url.IndexOf("https:");
                    int end = url.IndexOf("?format", start);
                    parsed_url = url.Substring(start, end - start);
                }

                // plain url
                else if (url.Contains("https:"))
                {
                    parsed_url = url.Substring(url.IndexOf("https:"), url.Length - url.IndexOf("https:"));
                }

                // try to read the data
                using (WebClient client = new WebClient())
                {
                    json = client.DownloadString(parsed_url);
                }
                
                // parse json to halbot activity and add to DB
                if(!string.IsNullOrEmpty(json))
                {
                    HalbotActivity activity = _TomTomJsonToHalbotActivity(json);
                    if(!_dbcontext.DBActivities.Contains(activity))
                    { 
                        _dbcontext.DBActivities.Add(activity);
                    }
                }
            }

            // persist changes in DB
            _dbcontext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private static HalbotActivity _TomTomJsonToHalbotActivity(string json)
        {

            // read json to anonymous object
            dynamic obj = JsonConvert.DeserializeObject(json);
            if (obj.id is null) return null;

            // create empty activity
            HalbotActivity activity = new HalbotActivity();

            // parse anonymous object
            if (obj.id != null) activity.ID = (int)obj.id;
            if (obj.aggregates.climb_total != null) activity.Climb = (int)obj.aggregates.climb_total;
            if (obj.aggregates.descent_total != null) activity.Descent = (int)obj.aggregates.descent_total;
            if (obj.start_datetime_user != null) activity.Date = (DateTime)obj.start_datetime_user;
            if (obj.aggregates.distance_total != null) activity.Distance = (int)obj.aggregates.distance_total;
            if (obj.aggregates.active_time_total != null) activity.Duration = (int)obj.aggregates.active_time_total;
            if (obj.aggregates.heartrate_avg != null) activity.Heartrate = (int)obj.aggregates.heartrate_avg;
            if (obj.links.image != null) activity.Image = (string)obj.links.image;
            if (obj.bounding_box != null) activity.Lat = ((float)obj.bounding_box.north_east.lat + (float)obj.bounding_box.south_west.lat) / 2;
            if (obj.bounding_box != null) activity.Lng = ((float)obj.bounding_box.north_east.lng + (float)obj.bounding_box.south_west.lng) / 2;
            if (obj.aggregates.speed_avg != null) activity.Speed = (float)obj.aggregates.speed_avg;
            if (obj.links.self != null) activity.Url = (string)obj.links.self;

            return activity;
        }
    }
}