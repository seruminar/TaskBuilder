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
        IFunctionModel BuildFunctionModel(Type functionType, Guid functionTypeGuid);

        bool TryBuildInvokeModel(MethodInfo invokeMethod, out IInvokeModel invokeModel);

        bool TryBuildDispatchModel(PropertyInfo dispatchProperty, out IDispatchModel dispatchModel);

        bool TryBuildInputModel(PropertyInfo inputProperty, string functionFullName, Guid functionTypeGuid, out IInputModel inputModel);

        bool TryBuildOutputModel(PropertyInfo outputProperty, string functionFullName, out IOutputModel outputModel);
    }
}