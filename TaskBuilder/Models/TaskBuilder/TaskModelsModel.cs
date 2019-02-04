using System;
using System.Collections.Generic;

using TaskBuilder.Models.Function;

namespace TaskBuilder.Models.TaskBuilder
{
    public class TaskModelsModel
    {
        public IEnumerable<IFunctionModel> Functions { get; }

        public IEnumerable<Guid> AuthorizedFunctionGuids { get; }

        public ICollection<string> Ports { get; }

        public ICollection<string> Links { get; }

        public TaskModelsModel(IEnumerable<IFunctionModel> functions, IEnumerable<Guid> authorizedFunctionGuids, ICollection<string> ports, ICollection<string> links)
        {
            Functions = functions;
            AuthorizedFunctionGuids = authorizedFunctionGuids;
            Ports = ports;
            Links = links;
        }
    }
}