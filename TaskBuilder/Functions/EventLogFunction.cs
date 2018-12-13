﻿using System;

using CMS.EventLog;

using TaskBuilder.Attributes;

namespace TaskBuilder.Functions
{
    public class EventLogFunction : Function
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
        [Input]
        public Func<string> TargetInParameter { get; set; }
    }
}