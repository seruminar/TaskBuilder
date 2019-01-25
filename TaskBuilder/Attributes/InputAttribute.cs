using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class InputAttribute : PortAttribute
    {
        internal Type ValueBuilder { get; }

        internal bool InlineOnly { get; }

        internal object[] StructureModelParams { get; }

        internal object[] FilledModelParams { get; }

        public InputAttribute(string displayName = null, string description = null) : base(displayName, description)
        {
        }

        public InputAttribute(Type valueBuilder, object[] structureModelParams, object[] filledModelParams = null, bool inlineOnly = false) : this(null, null, valueBuilder, structureModelParams, filledModelParams, inlineOnly)
        {
        }

        public InputAttribute(string displayName, Type valueBuilder, object[] structureModelParams, object[] filledModelParams = null, bool inlineOnly = false) : this(displayName, null, valueBuilder, structureModelParams, filledModelParams, inlineOnly)
        {
        }

        public InputAttribute(string displayName, string description, Type valueBuilder, object[] structureModelParams, object[] filledModelParams = null, bool inlineOnly = false) : base(displayName, description)
        {
            if (valueBuilder == null)
                throw new ArgumentNullException(nameof(ValueBuilder));

            ValueBuilder = valueBuilder;
            InlineOnly = inlineOnly;
            StructureModelParams = structureModelParams;
            FilledModelParams = filledModelParams;
        }
    }
}