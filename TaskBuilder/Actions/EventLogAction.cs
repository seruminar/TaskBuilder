using System;

using CMS.EventLog;

using TaskBuilder.Attributes;
using TaskBuilder.Models;

namespace TaskBuilder.Actions
{
    public class EventLogAction : TaskAction
    {
        [InReceiver]
        public void TargetInReceiver()
        {
            // Receive parameters
            var fromSource = TargetInParameter();

            // Set up parameters

            // Send / Execute
            EventLogProvider.LogInformation(nameof(EventLogAction), "TESTLOG", fromSource);
        }

        // This must be linked as a reactive parameter
        [InParameter]
        public Func<string> TargetInParameter { get; set; }

        public EventLogAction(Node node) : base(node)
        {
        }
    }
}