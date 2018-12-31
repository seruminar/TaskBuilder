using System;

using Newtonsoft.Json;

namespace TaskBuilder.Models
{
    public class Point
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }

        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }
    }
}