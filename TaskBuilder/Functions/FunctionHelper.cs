using System;
using System.Collections.Generic;
using System.Drawing;

namespace TaskBuilder.Functions
{
    public static class FunctionHelper
    {
        internal const string CALLER = "caller";
        internal const string PARAMETER = "parameter";
        internal const string INVOKE = "invoke";
        internal const string DISPATCH = "dispatch";
        internal const string INPUT = "input";
        internal const string OUTPUT = "output";

        public static ICollection<string> LinkTypes = new List<string>()
        {
            CALLER,
            PARAMETER
        };

        public static ICollection<string> PortTypes = new List<string>()
        {
            INVOKE,
            DISPATCH,
            INPUT,
            OUTPUT
        };

        public static IDictionary<string, Color> DisplayColors = new Dictionary<string, Color>()
        {
            { nameof(String), Color.SandyBrown }
        };
    }
}