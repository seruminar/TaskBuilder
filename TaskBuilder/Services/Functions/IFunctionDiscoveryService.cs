using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CMS;

using TaskBuilder.Services.Functions;

[assembly: RegisterImplementation(typeof(IFunctionDiscoveryService), typeof(FunctionDiscoveryService), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services.Functions
{
    /// <summary>
    /// Finds all of the loaded types that use <see cref="FunctionAttribute"/>.
    /// </summary>
    public interface IFunctionDiscoveryService
    {
        Type GetFunctionType(string functionAssembly, string functionFullName);

        Task<IEnumerable<Type>> DiscoverFunctionTypes();
    }
}