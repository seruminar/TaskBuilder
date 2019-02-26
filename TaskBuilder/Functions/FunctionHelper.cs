using System;
using System.Collections.Generic;
using System.Drawing;

using TaskBuilder.Models.Graph;

namespace TaskBuilder.Functions
{
    public static class FunctionHelper
    {
        public static ICollection<string> LinkTypes = new List<string>()
        {
            nameof(LinkType.Invoke).ToLower(),
            nameof(LinkType.Dispatch1).ToLower(),
            nameof(LinkType.Dispatch2).ToLower(),
            nameof(LinkType.Parameter).ToLower()
        };

        public static ICollection<string> PortTypes = new List<string>()
        {
            nameof(PortType.Invoke).ToLower(),
            nameof(PortType.Dispatch).ToLower(),
            nameof(PortType.Input).ToLower(),
            nameof(PortType.Output).ToLower()
        };

        public static IDictionary<string, Color> DisplayColors = new Dictionary<string, Color>()
        {
            { nameof(String), Color.SandyBrown }
        };
    }
}