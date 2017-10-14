using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Diagnostics;

namespace TechdaysPerformance.Tracing
{
    public class Trace
    {
        private readonly LoggingChannel channel;

        private Trace()
        {
            channel = new LoggingChannel("Techdays.Demo", new LoggingChannelOptions(), new Guid("174A1B24-F117-409D-BB57-6B178201ADCB"));
        }

        private static Lazy<Trace> instance = new Lazy<Trace>(() => new Trace());

        public static Trace Instance => instance.Value;

        public void LogMessage(string message)
        {
            // Don't log anything when no-one is listening.
            if (this.channel.Enabled)
            {
                this.channel.LogMessage(message);
            }
        }

        public LoggingActivity StartActivity(string activityName, LoggingFields fields = null)
        {
            return this.channel.StartActivity(activityName, fields ?? new LoggingFields());
        }
    }
}
