using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

using TaskBuilder.Models;
using TaskBuilder.Models.Function;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class InputAttribute : PortAttribute
    {
        public const string FACTORY_VALUE_OPTIONS_METHOD = "ValueOptions";
        public const string FACTORY_DEFAULT_VALUE_METHOD = "DefaultValue";

        internal InputType InputType { get; }

        internal Type ValueFactory { get; }

        internal InputValueModel DefaultValue { get; }

        internal IEnumerable<InputValueModel> ValueOptions { get; }

        public InputAttribute(string displayName = null) : this(displayName, null, null, null)
        {
        }

        public InputAttribute(Type valueFactory, object[] defaultValueParams = null, object[] valueOptionsParams = null) : this(null, valueFactory, defaultValueParams, valueOptionsParams)
        {
        }

        public InputAttribute(string displayName, Type valueFactory, object[] defaultValueParams = null, object[] valueOptionsParams = null)
        {
            InputType = InputType.Bare;

            if (!string.IsNullOrEmpty(displayName))
            {
                DisplayName = displayName;
                InputType = InputType.Plain;
            }

            if (valueFactory != null)
            {
                ValueFactory = valueFactory;

                if (defaultValueParams != null)
                {
                    DefaultValue = TryGetDefaultValue(valueFactory, defaultValueParams);
                    InputType = InputType.Field;
                }

                if (valueOptionsParams != null)
                {
                    ValueOptions = TryGetValueOptions(valueFactory, valueOptionsParams);
                    InputType = InputType.Dropdown;
                }
            }
            else throw new ArgumentNullException(nameof(ValueFactory));
        }

        private InputValueModel TryGetDefaultValue(Type valueFactory, object[] defaultValueParams)
        {
            var factory = FormatterServices.GetUninitializedObject(valueFactory);
            return valueFactory.InvokeMember(
                FACTORY_DEFAULT_VALUE_METHOD,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                null,
                factory,
                defaultValueParams)
                as InputValueModel;
        }

        private IEnumerable<InputValueModel> TryGetValueOptions(Type valueFactory, object[] valueOptionsParams)
        {
            var factory = FormatterServices.GetUninitializedObject(valueFactory);
            return valueFactory.InvokeMember(
                FACTORY_VALUE_OPTIONS_METHOD,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                null,
                factory,
                valueOptionsParams)
                as IEnumerable<InputValueModel>;
        }
    }
}