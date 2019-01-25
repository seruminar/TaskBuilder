using System;
using CMS.EventLog;

using TaskBuilder.Attributes;
using TaskBuilder.ValueBuilders;

namespace TaskBuilder.Functions.Implementations
{
    [Function("Log event", 0, 0, 255)]
    public struct LogEventFunction : IInvokable
    {
        public void Invoke()
        {
            // Receive parameters
            var fromSource = TargetInParameter();

            // Set up parameters

            // Send / Execute
            EventLogProvider.LogEvent(EventRecordType(), nameof(LogEventFunction), "TESTLOG", fromSource);
        }

        // This must be linked as a reactive parameter
        [Input("Description", null, typeof(StringValueBuilder), new object[] { "description" }, new object[] { "default" })]
        public Func<string> TargetInParameter { get; set; }

        [Input(typeof(StringValueOptionsBuilder), new object[] { EventType.INFORMATION, EventType.WARNING, EventType.ERROR }, new object[] { EventType.WARNING }, true)]
        public Func<string> EventRecordType { get; set; }
    }
}