using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using TaskBuilder.Models.Function;

namespace TaskBuilder.Models.TaskBuilder
{
    public class TaskModelsModel
    {
        public IEnumerable<IFunctionModel> Functions { get; }

        public IEnumerable<Guid> AuthorizedFunctionGuids { get; }

        public IEnumerable<string> Ports { get; }

        public IEnumerable<string> Links { get; }

        [JsonConstructor]
        public TaskModelsModel(IEnumerable<FunctionModel> functions, IEnumerable<Guid> authorizedFunctionGuids, IEnumerable<string> ports, IEnumerable<string> links)
        {
            Functions = functions;
            AuthorizedFunctionGuids = authorizedFunctionGuids;
            Ports = ports;
            Links = links;
        }
    }
}