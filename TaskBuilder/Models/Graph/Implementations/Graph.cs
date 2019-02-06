using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using TaskBuilder.Models.Function.InputValue;

namespace TaskBuilder.Models.Graph
{
    public class Graph : IGraph
    {
        public Guid Id { get; set; }

        public IEnumerable<ILink> Links { get; }

        public IEnumerable<INode> Nodes { get; }

        public IDictionary<Guid, IInputValueModel> InputValues { get; }

        [JsonConstructor]
        public Graph(Guid id, IEnumerable<Link> links, IEnumerable<Node> nodes, IDictionary<Guid, InputValueModel> inputValues) : this(id)
        {
            Links = links;
            Nodes = nodes;
            InputValues = inputValues.ToDictionary(k => k.Key, v => v.Value as IInputValueModel);
        }

        public Graph(Guid taskGuid)
        {
            Id = taskGuid;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, TaskBuilderHelper.JsonSerializerSettings);
        }
    }
}