using TaskBuilder.Models.Graph;
using TaskBuilder.Tasks;

namespace TaskBuilder.Models.TaskBuilder
{
    public class TaskGraphModel
    {
        public IGraph Graph { get; }

        public TaskGraphMode Mode { get; }

        public TaskGraphModel(Graph.Graph graph, TaskGraphMode mode)
        {
            Graph = graph;
            Mode = mode;
        }
    }
}