using Newtonsoft.Json;

namespace TaskBuilder.Models.Function
{
    public enum PortTypeEnum
    {
        [JsonProperty("enter")]
        Enter,

        [JsonProperty("leave")]
        Leave,

        [JsonProperty("input")]
        Input,

        [JsonProperty("output")]
        Output
    }
}