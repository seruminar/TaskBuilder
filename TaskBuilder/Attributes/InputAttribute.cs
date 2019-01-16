using System;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class InputAttribute : PortAttribute
    {
        internal Type ValueFactory { get; }

        internal object[] DefaultValueParams { get; }

        internal object[] ValueOptionsParams { get; }

        public InputAttribute(string displayName = null, string description = null) : this(displayName, description, null, null, null)
        {
        }

        public InputAttribute(Type valueFactory, object[] defaultValueParams = null, object[] valueOptionsParams = null) : this(null, null, valueFactory, defaultValueParams, valueOptionsParams)
        {
        }

        public InputAttribute(string displayName, string description, Type valueFactory, object[] defaultValueParams = null, object[] valueOptionsParams = null) : base(displayName, description)
        {
            if (valueFactory == null)
                throw new ArgumentNullException(nameof(ValueFactory));

            ValueFactory = valueFactory;
            DefaultValueParams = defaultValueParams;
            ValueOptionsParams = valueOptionsParams;
        }
    }
}