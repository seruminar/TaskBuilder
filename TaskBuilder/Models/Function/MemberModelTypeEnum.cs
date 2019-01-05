using Newtonsoft.Json;

namespace TaskBuilder.Models.Function
{
    internal enum MemberModelTypeEnum
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