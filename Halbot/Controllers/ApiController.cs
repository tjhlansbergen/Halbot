using System.Collections.Generic;
using System.Linq;
using Halbot.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Halbot.Controllers
{
    [Route("api/activities")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly DatabaseContext _dbcontext = new DatabaseContext();

        // GET: api/activities
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return ActivityCache.Get(_dbcontext).Select(a => a.Id.ToString()).ToArray();
        }

        // GET api/activities/5
        [HttpGet("{id}")]
        public string Get(long id)
        {
            var obj = ActivityCache.Get(_dbcontext).Single(a => a.Id == id);
            return JsonConvert.SerializeObject(obj);
        }
    }
}
