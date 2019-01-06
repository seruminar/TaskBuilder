using System;
using System.Collections.Generic;
using System.Drawing;

using Newtonsoft.Json;

namespace TaskBuilder.Models.Diagram
{
    public class ColoredModel : TypedModel, IColoredModel
    {
        protected readonly Color DEFAULT_DISPLAY_COLOR = Color.FromArgb(127, 255, 255, 255);

        protected static readonly Dictionary<string, Color?> _displayColors = new Dictionary<string, Color?>() {
            { nameof(String), Color.SaddleBrown }
        };

        [JsonProperty("displayColor")]
        public string DisplayColor { get; protected set; }
    }
}