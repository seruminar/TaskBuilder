using System;
using System.Collections.Generic;

using CMS;

using TaskBuilder.Services;

[assembly: RegisterImplementation(typeof(IFunctionModelService), typeof(FunctionModelService), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services
{
    public interface IFunctionModelService
    {
        IEnumerable<FunctionModel> FunctionModels { get; }

        FunctionModel GetFunctionModel(string functionName);

        List<Type> DiscoverFunctionTypes();

        HashSet<FunctionModel> RegisterFunctionModels();
    }
}