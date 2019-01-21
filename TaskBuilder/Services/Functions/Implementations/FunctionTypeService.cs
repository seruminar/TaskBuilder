using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using CMS.Core;

using TaskBuilder.Attributes;

namespace TaskBuilder.Services.Functions
{
    public class FunctionTypeService : IFunctionTypeService
    {
        private IDictionary<Guid, Type> _functionTypes;

        public Type GetFunctionType(Guid functionTypeIdentifier)
        {
            return _functionTypes[functionTypeIdentifier];
        }

        public IEnumerable<Guid> GetFilteredFunctionIdentifiers(Func<Type, bool> whereOperation)
        {
            return _functionTypes.Where(p => whereOperation(p.Value)).Select(p => p.Key);
        }

        public async Task<IDictionary<Guid, Type>> DiscoverFunctionTypes()
        {
            // Get loaded assemblies
            var discoveredAssemblies = AssemblyDiscoveryHelper.GetAssemblies(false);

            var functionTypes = new List<Type>();

            if (discoveredAssemblies.Any())
            {
                await Task.Run(() =>
                {
                    foreach (var assembly in discoveredAssemblies)
                    {
                        // Get list of classes from selected assembly
                        Type[] assemblyClassTypes;
                        try
                        {
                            assemblyClassTypes = assembly.GetTypes();
                        }
                        catch (ReflectionTypeLoadException exception)
                        {
                            assemblyClassTypes = exception.Types;
                        }

                        foreach (Type type in assemblyClassTypes.Where(x => x != null))
                        {
                            if (Attribute.IsDefined(type, typeof(FunctionAttribute)))
                                functionTypes.Add(type);
                        }
                    }
                }
                );
            }

            _functionTypes = functionTypes.ToDictionary(_ => Guid.NewGuid());

            return _functionTypes;
        }
    }
}