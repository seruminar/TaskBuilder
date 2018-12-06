using System;
using CMS.EventLog;
using TaskBuilder.Attributes;
using TaskBuilder.Models;

namespace TaskBuilder.Actions
{
    public class EventLogAction : TaskAction
    {
        public EventLogAction(Node node) : base(node)
        {
        }

        [InReceiver]
        public void TargetInReceiver()
        {
            // Receive parameters
            var fromSource = TargetInParameter();

            // Set up parameters
            EventLogProvider.LogInformation(nameof(EventLogAction), "TESTLOG", fromSource);

            // Send

        }

        // This must be linked as a reactive parameter
        [InParameter]
        public Func<string> TargetInParameter { get; set; }
    }
}