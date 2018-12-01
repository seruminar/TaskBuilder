using System;
using System.Collections.Generic;
using TaskBuilder.Models;

namespace TaskBuilder.Actions
{
    public class TaskAction
    {
        public Guid Guid { get; set; }

        public object Data { get; set; }

        public List<TaskAction> Targets { get; set; }

        public TaskAction(Node node)
        {
            Targets = new List<TaskAction>();
            Guid = node.Id;
        }

        public void Execute(object data)
        {
            ActOnData(data);

            if (Targets != null)
            {
                foreach (var node in Targets)
                {
                    node.Execute(Data);
                }
            }
        }

        protected virtual void ActOnData(object data)
        {
            Data = data;
        }
    }
}