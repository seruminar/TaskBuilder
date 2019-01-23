using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security;

using CMS.EventLog;

using TaskBuilder.Models.Function.InputValue;

namespace TaskBuilder.Services.Inputs
{
    public class InputValueService : IInputValueService
    {
        private readonly IDictionary<string, Type> _valueBuilders = new Dictionary<string, Type>();
        private readonly IDictionary<string, object[]> _defaultValueParams = new Dictionary<string, object[]>();

        public void StoreValueBuilder(string functionTypeIdentifier, string inputName, Type valueFactory)
        {
            if (!_valueBuilders.ContainsKey($"{functionTypeIdentifier}.{inputName}"))
            {
                _valueBuilders.Add($"{functionTypeIdentifier}.{inputName}", valueFactory);
            }
        }

        public dynamic BuildValue(string functionTypeIdentifier, string inputName, IInputValueModel filledModel)
        {
            try
            {
                var builderType = _valueBuilders[$"{functionTypeIdentifier}.{inputName}"];

                var builder = FormatterServices.GetUninitializedObject(builderType);

                var constructValue = builderType
                    .GetMethod(nameof(IValueBuilder<object>.BuildValue));

                return constructValue.Invoke(builder, new[] { filledModel });
            }
            catch (SecurityException ex)
            {
                EventLogProvider.LogException(TaskBuilderHelper.TASKBUILDER, nameof(BuildValue).ToUpper(), ex);
            }

            return null;
        }

        public bool TryGetStructureModel(Type builderType, out IInputValueModel structureModel, dynamic[] structureModelParams)
        {
            try
            {
                var builder = FormatterServices.GetUninitializedObject(builderType);

                var structureModelMethod = builderType
                    .GetMethod(nameof(IValueBuilder<object>.GetStructureModel));

                structureModel = structureModelMethod.Invoke(builder, new[] { structureModelParams }) as IInputValueModel;
            }
            catch (SecurityException ex)
            {
                EventLogProvider.LogException(TaskBuilderHelper.TASKBUILDER, nameof(TryGetStructureModel).ToUpper(), ex);
                structureModel = null;
            }

            if (structureModel == null) return false;

            return true;
        }

        public bool TryGetFilledModel(Type builderType, IInputValueModel structureModel, out IInputValueModel filledModel, dynamic[] filledModelParams)
        {
            try
            {
                var builder = FormatterServices.GetUninitializedObject(builderType);

                var filledModelMethod = builderType
                    .GetMethod(nameof(IFilledValueBuilder<object>.GetFilledModel));

                filledModel = filledModelMethod.Invoke(builder, new object[] { structureModel, filledModelParams }) as IInputValueModel;
            }
            catch (SecurityException ex)
            {
                EventLogProvider.LogException(TaskBuilderHelper.TASKBUILDER, nameof(TryGetFilledModel).ToUpper(), ex);
                filledModel = null;
            }

            if (filledModel == null) return false;

            return true;
        }
    }
}