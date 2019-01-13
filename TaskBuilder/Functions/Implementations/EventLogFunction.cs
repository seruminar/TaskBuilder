using System;
using CMS.EventLog;

using TaskBuilder.Attributes;

namespace TaskBuilder.Functions.Implementations
{
    [Function(0, 0, 255)]
    public struct EventLogFunction
    {
        public void Invoke()
        {
            // Receive parameters
            var fromSource = TargetInParameter();

            // Set up parameters

            // Send / Execute
            EventLogProvider.LogEvent(EventRecordType(), nameof(EventLogFunction), "TESTLOG", fromSource);
        }

        // This must be linked as a reactive parameter
        [Input("Description", "default")]
        public Func<string> TargetInParameter { get; set; }

        [Input(new object[] { EventType.INFORMATION, EventType.ERROR }, defaultValue: EventType.INFORMATION)]
        public Func<string> EventRecordType { get; set; }
    }
}