using System;
using System.Collections.Generic;

using TaskBuilder.Models.Function.InputValue;

namespace TaskBuilder.Models.Graph
{
    public interface IGraph
    {
        Guid Id { get; set; }

        IEnumerable<ILink> Links { get; }

        IEnumerable<INode> Nodes { get; }

        IDictionary<Guid, IInputValueModel> InputValues { get; }

        string ToJson();
    }
}