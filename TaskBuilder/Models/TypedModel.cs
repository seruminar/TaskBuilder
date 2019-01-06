using Newtonsoft.Json;

namespace TaskBuilder.Models
{
    public class TypedModel : BaseModel, ITypedModel
    {
        [JsonProperty("displayType")]
        public string DisplayType { get; protected set; }
    }
}