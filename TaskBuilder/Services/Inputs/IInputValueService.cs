using System;

using CMS;

using TaskBuilder.Models.Function.InputValue;
using TaskBuilder.Services.Inputs;

[assembly: RegisterImplementation(typeof(IInputValueService), typeof(InputValueService), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services.Inputs
{
    public interface IInputValueService
    {
        void StoreValueBuilder(string functionTypeIdentifier, string inputName, Type valueFactory);

        dynamic BuildValue(string functionTypeIdentifier, string inputName, IInputValueModel filledModel);

        bool TryGetStructureModel(Type builderType, out IInputValueModel structureModel, dynamic[] structureModelParams);

        bool TryGetFilledModel(Type builderType, IInputValueModel structureModel, out IInputValueModel filledModel, dynamic[] filledModelParams);
    }
}