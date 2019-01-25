using System;
using System.Collections.Generic;
using System.Drawing;

namespace TaskBuilder.Functions
{
    public static class FunctionHelper
    {
        internal const string LINK_DISPATCH = nameof(IDispatcher.Dispatch);
        internal const string LINK_DISPATCH2 = nameof(IDispatcher2.Dispatch2);
        internal const string LINK_PARAMETER = "Parameter";
        internal const string INVOKE = nameof(IInvokable.Invoke);
        internal const string DISPATCH = nameof(IDispatcher.Dispatch);
        internal const string INPUT = "Input";
        internal const string OUTPUT = "Output";

        public static ICollection<string> LinkTypes = new List<string>()
        {
            LINK_DISPATCH,
            LINK_DISPATCH2,
            LINK_PARAMETER
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