using System;
using System.Reflection;

using CMS;

using TaskBuilder.Models.Function;
using TaskBuilder.Services;

[assembly: RegisterImplementation(typeof(IFunctionModelBuilder), typeof(FunctionModelBuilder), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services
{
    public interface IFunctionModelBuilder
    {
        IFunctionModel BuildSimpleFunctionModel(string name);

        IFunctionModel BuildFunctionModel(Type functionType);

        bool TryBuildInvokeModel(MethodInfo invokeMethod, out IInvokeModel invokeModel);

        bool TryBuildDispatchModel(PropertyInfo dispatchProperty, out IDispatchModel dispatchModel);

        bool TryBuildInputModel(PropertyInfo inputProperty, string functionFullName, out IInputModel inputModel);

        bool TryBuildOutputModel(PropertyInfo outputProperty, string functionFullName, out IOutputModel outputModel);
    }
}