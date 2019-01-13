using System;
using System.Reflection;
using System.Runtime.Serialization;
using TaskBuilder.Models;
using TaskBuilder.Models.Function;

namespace TaskBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class InputAttribute : PortAttribute
    {
        private readonly string INPUT_OPTIONS_FACTORY_METHOD_NAME = "InputOptions";

        public object DefaultValue { get; }

        public InputType InputType { get; }

        public InputOptionModel[] InputOptions { get; }

        public InputAttribute(InputType inputType) : this(null, inputType)
        {
        }

        public InputAttribute(string displayName, InputType inputType = InputType.Automatic) : this(displayName, null, inputType: inputType)
        {
        }

        public InputAttribute(object defaultValue, InputType inputType = InputType.Automatic) : this(null, defaultValue, inputType)
        {
        }

        public InputAttribute(string displayName, object defaultValue, InputType inputType = InputType.Automatic)
        {
            DisplayName = displayName;
            DefaultValue = defaultValue;
            InputType = inputType;
        }

        public InputAttribute(Type inputOptionsFactory) : this(null, inputOptionsFactory)
        {
        }

        public InputAttribute(Type inputOptionsFactory, object defaultValue) : this(null, inputOptionsFactory, defaultValue)
        {
        }

        public InputAttribute(string displayName, Type inputOptionsFactory) : this(displayName, inputOptionsFactory, null)
        {
        }

        public InputAttribute(string displayName, Type inputOptionsFactory, object defaultValue)
        {
            DisplayName = displayName;
            DefaultValue = defaultValue;
            InputOptions = TryGetInputOptions(inputOptionsFactory);
        }

        public InputAttribute(object[] inputOptions) : this(null, inputOptions)
        {
        }

        public InputAttribute(object[] inputOptions, object defaultValue) : this(null, inputOptions, defaultValue)
        {
        }

        public InputAttribute(string displayName, object[] inputOptions) : this(displayName, inputOptions, null)
        {
        }

        public InputAttribute(string displayName, object[] inputOptions, object defaultValue)
        {
            DisplayName = displayName;
            DefaultValue = defaultValue;
            InputOptions = Array.ConvertAll(inputOptions, new Converter<object, InputOptionModel>(obj => new InputOptionModel(obj)));
        }

        private InputOptionModel[] TryGetInputOptions(Type inputOptionsFactory)
        {
            var factory = FormatterServices.GetUninitializedObject(inputOptionsFactory);
            return inputOptionsFactory.InvokeMember(INPUT_OPTIONS_FACTORY_METHOD_NAME, BindingFlags.Public | BindingFlags.Instance, null, factory, null) as InputOptionModel[];
        }
    }
}