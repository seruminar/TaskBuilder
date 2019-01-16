using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security;
using CMS.EventLog;
using TaskBuilder.Models.Function;

namespace TaskBuilder.Services
{
    public class InputValueService : IInputValueService
    {
        private readonly IDictionary<string, Type> _valueFactories = new Dictionary<string, Type>();
        private readonly IDictionary<string, object[]> _defaultValueParams = new Dictionary<string, object[]>();

        public void StoreValueFactory(string functionName, string inputName, Type valueFactory)
        {
            if (!_valueFactories.ContainsKey($"{functionName}.{inputName}"))
            {
                _valueFactories.Add($"{functionName}.{inputName}", valueFactory);
            }
        }

        public object ConstructValue(string functionName, string inputName, string valueData)
        {
            var valueFactory = _valueFactories[$"{functionName}.{inputName}"];

            var factory = FormatterServices.GetUninitializedObject(valueFactory) as IValueFactory;

            return factory.ConstructValue(valueData);
        }

        public bool TryGetDefaultValue(Type valueFactory, out IInputValueModel defaultValue, object[] defaultValueParams)
        {
            try
            {
                var factory = FormatterServices.GetUninitializedObject(valueFactory) as IValueFactory;

                defaultValue = factory?.GetDefaultValue(defaultValueParams) as InputValueModel;
            }
            catch (SecurityException ex)
            {
                EventLogProvider.LogException(TaskBuilderHelper.TASKBUILDER, nameof(TryGetDefaultValue).ToUpper(), ex);
                defaultValue = null;
            }

            if (defaultValue == null) return false;

            return true;
        }

        public bool TryGetValueOptions(Type valueFactory, out IEnumerable<IInputValueModel> valueOptions, object[] valueOptionsParams)
        {
            try
            {
                var factory = FormatterServices.GetUninitializedObject(valueFactory) as IValueFactory;

                valueOptions = factory?.GetValueOptions(valueOptionsParams) as IEnumerable<InputValueModel>;
            }
            catch (SecurityException ex)
            {
                EventLogProvider.LogException(TaskBuilderHelper.TASKBUILDER, nameof(TryGetValueOptions).ToUpper(), ex);
                valueOptions = null;
            }

            if (valueOptions == null) return false;

            return true;
        }
    }
}