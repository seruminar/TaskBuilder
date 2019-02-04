using System.Collections.Generic;

namespace TaskBuilder.Models.TaskBuilder
{
    public class TaskBuilderModel
    {
        public TaskModelsModel Models { get; set; }

        public TaskGraphModel Graph { get; set; }

        public IDictionary<string, string> Endpoints { get; set; }

        public string SecureToken { get; set; }
    }
}