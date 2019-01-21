using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security;
using CMS.EventLog;
using TaskBuilder.Models.Function;

namespace TaskBuilder.Services.Inputs
{
    public class InputValueService : IInputValueService
    {
        private readonly IDictionary<string, Type> _valueBuilders = new Dictionary<string, Type>();
        private readonly IDictionary<string, object[]> _defaultValueParams = new Dictionary<string, object[]>();

        public void StoreValueBuilder(Guid functionTypeIdentifier, string inputName, Type valueFactory)
        {
            if (!_valueBuilders.ContainsKey($"{functionTypeIdentifier}.{inputName}"))
            {
                _valueBuilders.Add($"{functionTypeIdentifier}.{inputName}", valueFactory);
            }
        }

        public dynamic ConstructValue(Guid functionTypeIdentifier, string inputName, InputFieldsModel fieldsModel)
        {
            try
            {
                var builderType = _valueBuilders[$"{functionTypeIdentifier}.{inputName}"];

                var builder = FormatterServices.GetUninitializedObject(builderType);

                var constructValue = builderType
                    .GetMethod(nameof(IValueBuilder<object>.ConstructValue));

                return constructValue.Invoke(builder, new[] { fieldsModel });
            }
            catch (SecurityException ex)
            {
                EventLogProvider.LogException(TaskBuilderHelper.TASKBUILDER, nameof(ConstructValue).ToUpper(), ex);
            }

            return null;
        }

        public bool TryGetEmptyFields(Type builderType, out InputFieldsModel emptyFieldsModel)
        {
            try
            {
                var builder = FormatterServices.GetUninitializedObject(builderType);

                var emptyValueModelMethod = builderType
                    .GetMethod(nameof(IValueBuilder<object>.NewFieldsModel));

                emptyFieldsModel = emptyValueModelMethod.Invoke(builder, null) as InputFieldsModel;
            }
            catch (SecurityException ex)
            {
                EventLogProvider.LogException(TaskBuilderHelper.TASKBUILDER, nameof(TryGetEmptyFields).ToUpper(), ex);
                emptyFieldsModel = null;
            }

            if (emptyFieldsModel == null) return false;

            return true;
        }

        public bool TryGetDefaultFields(Type builderType, out InputFieldsModel defaultFieldsModel, object[] defaultValueParams)
        {
            try
            {
                var builder = FormatterServices.GetUninitializedObject(builderType);

                var valueModelFrom = builderType
                    .GetMethod(nameof(IValueModelBuilder<object>.FieldsModelFrom));

                defaultFieldsModel = valueModelFrom.Invoke(builder, new[] { defaultValueParams }) as InputFieldsModel;
            }
            catch (SecurityException ex)
            {
                EventLogProvider.LogException(TaskBuilderHelper.TASKBUILDER, nameof(TryGetDefaultFields).ToUpper(), ex);
                defaultFieldsModel = null;
            }

            if (defaultFieldsModel == null) return false;

            return true;
        }

        public bool TryGetOptions(Type builderType, out InputFieldsModel optionsModel, object[] valueOptionsParams)
        {
            try
            {
                var builder = FormatterServices.GetUninitializedObject(builderType);

                var valueOptionsModelFrom = builderType
                    .GetMethod(nameof(IValueOptionsModelBuilder<object>.OptionsModelFrom));

                optionsModel = valueOptionsModelFrom.Invoke(builder, new[] { valueOptionsParams }) as InputFieldsModel;
            }
            catch (SecurityException ex)
            {
                EventLogProvider.LogException(TaskBuilderHelper.TASKBUILDER, nameof(TryGetOptions).ToUpper(), ex);
                optionsModel = null;
            }

            if (optionsModel == null) return false;

            return true;
        }
    }
}