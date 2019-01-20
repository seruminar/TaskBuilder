using System;
using CMS;
using TaskBuilder.Models.Function;
using TaskBuilder.Services.Inputs;

[assembly: RegisterImplementation(typeof(IInputValueService), typeof(InputValueService), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services.Inputs
{
    public interface IInputValueService
    {
        void StoreValueBuilder(string functionName, string inputName, Type valueFactory);

        dynamic ConstructValue(string functionName, string inputName, InputFieldsModel fieldsModel);

        bool TryGetEmptyFields(Type builderType, out InputFieldsModel emptyFieldsModel);

        bool TryGetDefaultFields(Type builderType, out InputFieldsModel defaultFieldsModel, object[] defaultValueParams);

        bool TryGetOptions(Type builderType, out InputFieldsModel optionsModel, object[] valueOptionsParams);
    }
}