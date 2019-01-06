using System.Diagnostics;
using Newtonsoft.Json;

namespace TaskBuilder.Models
{
    [DebuggerDisplay("{DisplayName}: {Type}")]
    public class BaseModel : IBaseModel
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; protected set; }

        [JsonProperty("type")]
        public string Type { get; protected set; }
    }
}