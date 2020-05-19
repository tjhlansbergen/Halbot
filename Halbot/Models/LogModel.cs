using Halbot.Data.Records;
using System.Collections.Generic;

namespace Halbot.Models
{
    public class LogModel
    {
        // properties
        public List<LogRecord> LogLines { get; }

        // constructor
        public LogModel(List<LogRecord> logLines)
        {
            //initialize
            LogLines = logLines;
        }

    }
}
