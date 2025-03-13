using Halbot.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Halbot.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly DatabaseContext _dbcontext = new DatabaseContext();
        private readonly Logger _logger = new Logger();

    }
}
