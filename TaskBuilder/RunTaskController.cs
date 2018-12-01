using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using TaskBuilder.Actions;

using TaskAction = TaskBuilder.Actions.TaskAction;

namespace TaskBuilder
{
    public class RunTaskController : ApiController
    {
        public void Post([FromBody] DiagramModel diagram)
        {
            var listOfNodes = new List<TaskAction>();

            foreach (var node in diagram.Nodes)
            {
                TaskAction createdNode = null;
                switch (node.Type)
                {
                    case "startNode":
                        createdNode = new StartAction(node);
                        break;

                    case "eventLogNode":
                        createdNode = new EventLogAction(node);
                        break;

                    default:
                        break;
                }

                listOfNodes.Add(createdNode);
            }

            foreach (var node in listOfNodes)
            {
                var link = diagram.Links.FirstOrDefault(l => l.Source == node.Guid);

                if (link != null)
                {
                    var targetNode = listOfNodes.FirstOrDefault(n => n.Guid == link.Target);

                    if (targetNode != null)
                    {
                        node.Targets.Add(targetNode);
                    }
                }
            }

            var startNode = listOfNodes.FirstOrDefault(n => n is StartAction);

            startNode?.Execute(null);
        }
    }
}