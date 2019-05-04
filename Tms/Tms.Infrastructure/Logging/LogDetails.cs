using System;
using System.Collections.Generic;
using System.Text;

namespace Tms.Infrastructure.Logging
{
    public class LogDetails
    {
        public LogDetails()
        {
            Timestamp = DateTime.Now;
        }
        public DateTime Timestamp { get; private set; }
        public string Message { get; set; }
        public string Product { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }
        public string User { get; set; }
        public Exception Exception { get; set; }
    }
}
