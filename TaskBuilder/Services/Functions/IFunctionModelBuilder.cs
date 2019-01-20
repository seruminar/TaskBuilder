using System;
using System.Reflection;

using CMS;

using TaskBuilder.Models.Function;
using TaskBuilder.Services.Functions;

[assembly: RegisterImplementation(typeof(IFunctionModelBuilder), typeof(FunctionModelBuilder), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services.Functions
{
    public interface IFunctionModelBuilder
    {
        FunctionModel BuildSimpleFunctionModel(string typeName, string assemblyName);

        FunctionModel BuildFunctionModel(Type functionType);

        bool TryBuildInvokeModel(MethodInfo invokeMethod, out CallerModel invokeModel);

        bool TryBuildDispatchModel(PropertyInfo dispatchProperty, out CallerModel dispatchModel);

        bool TryBuildInputModel(PropertyInfo inputProperty, string functionFullName, out InputModel inputModel);

        bool TryBuildOutputModel(PropertyInfo outputProperty, string functionFullName, out OutputModel outputModel);
    }
}