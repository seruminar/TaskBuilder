using System;
using System.Collections.Generic;
using CMS;
using TaskBuilder.Models.Function;
using TaskBuilder.Services;

[assembly: RegisterImplementation(typeof(IInputValueService), typeof(InputValueService), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services
{
    public interface IInputValueService
    {
        void StoreValueFactory(string functionName, string inputName, Type valueFactory);

        object ConstructValue(string functionName, string inputName, string valueData);

        bool TryGetDefaultValue(Type valueFactory, out IInputValueModel defaultValue, object[] defaultValueParams);

        bool TryGetValueOptions(Type valueFactory, out IEnumerable<IInputValueModel> valueOptions, object[] valueOptionsParams);
    }
}