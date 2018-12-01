using CMS.EventLog;

namespace TaskBuilder.Actions
{
    public class EventLogAction : TaskAction
    {
        public EventLogAction(Node node) : base(node)
        {
        }

        protected override void ActOnData(object data)
        {
            EventLogProvider.LogInformation("Task builder", "NODELOGINFO", "sample information");
            base.ActOnData(data);
        }
    }
}