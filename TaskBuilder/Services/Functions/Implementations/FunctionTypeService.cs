using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using CMS.Core;
using CMS.DataEngine;
using TaskBuilder.Attributes;

namespace TaskBuilder.Services.Functions
{
    public class FunctionTypeService : IFunctionTypeService
    {
        private IDictionary<Guid, Type> _guidTypes;

        public Type GetFunctionType(Guid functionTypeGuid)
        {
            return _guidTypes[functionTypeGuid];
        }

        public IEnumerable<Guid> GetFunctionGuids(Func<Guid, bool> whereCondition)
        {
            return _guidTypes.Keys.Where(g => whereCondition(g));
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

            using (var tr = new CMSTransactionScope())
            {
                IDictionary<Guid, Type> guidTypes = new Dictionary<Guid, Type>();

                var existingTypes = FunctionTypeInfoProvider
                                        .GetFunctionTypes();

                foreach (var type in functionTypes)
                {
                    // Type already exists in database
                    if (existingTypes.Any(t => FunctionTypeAndTypeAreEqual(t, type)))
                    {
                        guidTypes.Add(existingTypes.First(t => FunctionTypeAndTypeAreEqual(t, type)).FunctionTypeGuid, type);
                        continue;
                    }

                    // Add type to database
                    var typeInfo = new FunctionTypeInfo(type.FullName, type.Assembly.GetName().Name);

                    FunctionTypeInfoProvider.SetFunctionTypeInfo(typeInfo);

                    guidTypes.Add(typeInfo.FunctionTypeGuid, type);
                }

                foreach (var type in existingTypes)
                {
                    if (!functionTypes.Any(t => FunctionTypeAndTypeAreEqual(type, t)))
                    {
                        FunctionTypeInfoProvider.DeleteFunctionTypeInfo(type);
                    }
                }

                _guidTypes = guidTypes;

                tr.Commit();
            }

            return _guidTypes;
        }

        public bool FunctionTypeAndTypeAreEqual(FunctionTypeInfo functionTypeInfo, Type functionType)
        {
            return functionTypeInfo.FunctionTypeClass == functionType.FullName
                && functionTypeInfo.FunctionTypeAssembly == functionType.Assembly.GetName().Name;
        }
    }
}