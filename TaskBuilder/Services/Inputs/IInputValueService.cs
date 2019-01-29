using System;

using CMS;

using TaskBuilder.Models.Function.InputValue;
using TaskBuilder.Services.Inputs;

[assembly: RegisterImplementation(typeof(IInputValueService), typeof(InputValueService), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services.Inputs
{
    public interface IInputValueService
    {
        void StoreValueBuilder(Guid functionTypeGuid, string inputName, Type valueFactory);

        dynamic BuildValue(Guid functionTypeGuid, string inputName, IInputValueModel filledModel);

        bool TryGetStructureModel(Type builderType, out IInputValueModel structureModel, dynamic[] structureModelParams);

        bool TryGetFilledModel(Type builderType, IInputValueModel structureModel, out IInputValueModel filledModel, dynamic[] filledModelParams);
    }
}