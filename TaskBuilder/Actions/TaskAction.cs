using System;
using System.Collections.Generic;
using TaskBuilder.Models;

namespace TaskBuilder.Actions
{
    public abstract class TaskAction
    {
        public Guid Guid { get; set; }

        public TaskAction(Node node)
        {
            Guid = node.Id;
        }
    }
}