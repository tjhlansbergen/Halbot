using Halbot.Data;
using Halbot.Data.Records;
using System;
using System.Linq;

namespace Halbot.Controllers
{
    public class Logger
    {
        private readonly DatabaseContext _dbcontext = new DatabaseContext();

        public void Log(LogSeverityLevel severity, string message)
        {
            var line = new LogRecord
            {
                DateTime = DateTime.Now,
                Severity = severity,
                Message = message
            };

            _dbcontext.LogRecords.Add(line);

            if (_dbcontext.LogRecords.Count() > 50)
            {
                _dbcontext.LogRecords.RemoveRange(_dbcontext.LogRecords.OrderBy(l => l.DateTime).Take(10));
            }

            _dbcontext.SaveChanges();
        }
    }
}
