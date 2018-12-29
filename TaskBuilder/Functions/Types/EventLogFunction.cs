using System;

using CMS.EventLog;

using TaskBuilder.Attributes;

namespace TaskBuilder.Functions
{
    [Function]
    public class EventLogFunction
    {
        [Enter]
        public void TargetInReceiver()
        {
            // Receive parameters
            var fromSource = TargetInParameter();

            // Set up parameters

            // Send / Execute
            EventLogProvider.LogInformation(nameof(EventLogFunction), "TESTLOG", fromSource);
        }

        // This must be linked as a reactive parameter
        [Input("Description")]
        public Func<string> TargetInParameter { get; set; }
    }
}