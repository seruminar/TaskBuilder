using System;
using System.Collections.Generic;

using CMS;

using TaskBuilder.Services;

[assembly: RegisterImplementation(typeof(IFunctionDiscoveryService), typeof(FunctionDiscoveryService), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services
{
    /// <summary>
    /// Finds all of the loaded types that use <see cref="FunctionAttribute"/>.
    /// </summary>
    public interface IFunctionDiscoveryService
    {
        Type GetFunctionType(string functionFullName);

        IEnumerable<Type> DiscoverFunctionTypes();
    }
}