using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Utilities.Logging
{
    public static class Logger
    {

        public static Task Log(Exception ex, string extraInfo = null)
        {
            // New StringBuilder to hold logged text
            StringBuilder builder = new StringBuilder();

            // Append exception
            builder.Append(ex);

            if (ex is DbEntityValidationException)
            {
                var validationException = (DbEntityValidationException)ex;

                var errorMessages = validationException.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);

                string fullErrorMessage = String.Join("\n", errorMessages);

                builder.Append("\n------------------- Validation Errors ------------------- \n");

                builder.Append(fullErrorMessage);
            }

            // Create new Reference for inner exception
            Exception innerException = ex.InnerException;

            // While the innerException is not null
            while (innerException != null)
            {
                // Append Inner Exception
                builder.Append("\n------------------- Inner Exception ------------------- \n");

                builder.Append(innerException);

                // change innerException Reference to inner's innerException
                innerException = innerException.InnerException;
            }

            if (extraInfo != null)
            {
                builder.Append("\n------------------- Extra Info ------------------- \n");
                builder.Append(extraInfo);
            }

            return Log(builder.ToString());

        }

        public static Task Log(string logText, EventLogEntryType logType = EventLogEntryType.Error)
        {
            // Name of log source
            string source = "Web App";

            // Create source on application if not exist
            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, "Application");
            }

            // Write log entry
            return Task.Run(() =>
            {

                try
                {
                    EventLog.WriteEntry(source, logText, logType);
                }
                catch { }

            });

        }

        public static Task Info(string logText)
        {
            return Log(logText, EventLogEntryType.Information);
        }

        public static Task Warn(string logText)
        {
            return Log(logText, EventLogEntryType.Warning);
        }

    }
}
