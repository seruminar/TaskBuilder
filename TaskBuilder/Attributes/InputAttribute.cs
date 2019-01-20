using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class InputAttribute : PortAttribute
    {
        internal Type ValueBuilder { get; }

        internal bool InlineOnly { get; }

        internal object[] ValueParams { get; }

        internal object[] ValueOptionsParams { get; }

        public InputAttribute(string displayName = null, string description = null) : this(displayName, description, null, false, null, null)
        {
        }

        public InputAttribute(string displayName, Type valueFactory, bool inlineOnly = false, object[] valueParams = null, object[] valueOptionsParams = null) : this(displayName, null, valueFactory, false, valueParams, valueOptionsParams)
        {
        }

        public InputAttribute(Type valueBuilder, bool inlineOnly = false, object[] valueParams = null, object[] valueOptionsParams = null) : this(null, null, valueBuilder, false, valueParams, valueOptionsParams)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="description"></param>
        /// <param name="valueBuilder"></param>
        /// <param name="inlineOnly"></param>
        /// <param name="valueParams">For no default value, set to null.</param>
        /// <param name="valueOptionsParams"></param>
        public InputAttribute(string displayName, string description, Type valueBuilder, bool inlineOnly = false, object[] valueParams = null, object[] valueOptionsParams = null) : base(displayName, description)
        {
            if (valueBuilder == null)
                throw new ArgumentNullException(nameof(ValueBuilder));

            ValueBuilder = valueBuilder;
            InlineOnly = inlineOnly;
            ValueParams = valueParams;
            ValueOptionsParams = valueOptionsParams;
        }
    }
}