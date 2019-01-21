using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CMS;

using TaskBuilder.Services.Functions;

[assembly: RegisterImplementation(typeof(IFunctionTypeService), typeof(FunctionTypeService), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services.Functions
{
    /// <summary>
    /// Finds all of the loaded types that use <see cref="FunctionAttribute"/>.
    /// </summary>
    public interface IFunctionTypeService
    {
        Type GetFunctionType(Guid functionTypeIdentifier);

        IEnumerable<Guid> GetFilteredFunctionIdentifiers(Func<Type, bool> whereOperation);

        Task<IDictionary<Guid, Type>> DiscoverFunctionTypes();
    }
}