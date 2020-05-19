using System;

namespace Halbot.Data.Records
{
    public class LogRecord
    {
        public long Id { get; set; } // primary key
        public DateTime DateTime { get; set; }
        public LogSeverityLevel Severity { get; set; }
        public string Message { get; set; }
    }
}
