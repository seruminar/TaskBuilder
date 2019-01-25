using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using CMS.Core;
using CMS.Helpers;
using TaskBuilder.Attributes;

namespace TaskBuilder.Services.Functions
{
    public class FunctionTypeService : IFunctionTypeService
    {
        private IDictionary<string, Type> _functionTypes;

        public Type GetFunctionType(string functionTypeIdentifier)
        {
            return _functionTypes[functionTypeIdentifier];
        }

        public IEnumerable<string> GetFilteredFunctionIdentifiers(Func<Type, bool> whereOperation)
        {
            return _functionTypes.Where(p => whereOperation(p.Value)).Select(p => p.Key);
        }

        public async Task<IDictionary<string, Type>> DiscoverFunctionTypes()
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

            _functionTypes = functionTypes.ToDictionary(type => HashFunctionTypeIdentifier(type.AssemblyQualifiedName));

            return _functionTypes;
        }

        public string HashFunctionTypeIdentifier(string identifier)
        {
            return SecurityHelper.GetSHA2Hash(identifier);
        }
    }
}