using System;
using CMS.EventLog;

using TaskBuilder.Attributes;
using TaskBuilder.Services.ValueFactories;

namespace TaskBuilder.Functions.Implementations
{
    [Function(0, 0, 255)]
    public struct EventLogFunction : IInvokable
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
        [Input("Description", null, typeof(StringValueFactory), defaultValueParams: new object[] { "default" })]
        public Func<string> TargetInParameter { get; set; }

        [Input(typeof(StringValueFactory), new object[] { EventType.INFORMATION }, new object[] { EventType.INFORMATION, EventType.WARNING, EventType.ERROR })]
        public Func<string> EventRecordType { get; set; }
    }
}