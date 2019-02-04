using TaskBuilder.Tasks;

namespace TaskBuilder.Models.TaskBuilder
{
    public class TaskGraphModel
    {
        public string Json { get; }

        public TaskGraphMode Mode { get; }

        public TaskGraphModel(string taskGraphJson, TaskGraphMode taskGraphMode)
        {
            Json = taskGraphJson;
            Mode = taskGraphMode;
        }
    }
}