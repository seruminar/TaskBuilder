using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using CMS.Core;
using CMS.DataEngine;

using TaskBuilder.Attributes;
using TaskBuilder.Services.Inputs;

namespace TaskBuilder.Services.Functions
{
    public class FunctionTypeService : IFunctionTypeService
    {
        private readonly IDisplayConverter _displayConverter;

        private IDictionary<Guid, Type> _guidTypes;

        public FunctionTypeService(IDisplayConverter displayConverter)
        {
            _displayConverter = displayConverter;
        }

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

            _guidTypes = GetUpdatedGuidTypeDictionary(functionTypes);

            return _guidTypes;
        }

        private IDictionary<Guid, Type> GetUpdatedGuidTypeDictionary(List<Type> functionTypes)
        {
            using (var tr = new CMSTransactionScope())
            {
                IDictionary<Guid, Type> guidTypes = new ConcurrentDictionary<Guid, Type>();

                var existingTypes = FunctionTypeInfoProvider.GetFunctionTypes();

                Parallel.ForEach(functionTypes, type =>
                {
                    // Type already exists in database
                    if (existingTypes.Any(t => FunctionTypeAndTypeAreEqual(t, type)))
                    {
                        guidTypes.Add(existingTypes.First(t => FunctionTypeAndTypeAreEqual(t, type)).FunctionTypeGuid, type);
                        return;
                    }

                    // Add type to database
                    var attribute = type.GetCustomAttribute<FunctionAttribute>();

                    var displayName = _displayConverter.DisplayNameFrom(attribute.DisplayName, type.FullName, type.Name);

                    var typeInfo = new FunctionTypeInfo(displayName, type.AssemblyQualifiedName);

                    FunctionTypeInfoProvider.SetFunctionTypeInfo(typeInfo);

                    guidTypes.Add(typeInfo.FunctionTypeGuid, type);
                });

                foreach (var type in existingTypes)
                {
                    if (!functionTypes.Any(t => FunctionTypeAndTypeAreEqual(type, t)))
                    {
                        FunctionTypeInfoProvider.DeleteFunctionTypeInfo(type);
                    }
                }
                tr.Commit();

                return guidTypes;
            }
        }

        public bool FunctionTypeAndTypeAreEqual(FunctionTypeInfo functionTypeInfo, Type functionType)
        {
            return functionTypeInfo.FunctionTypeAssemblyQualifiedName == functionType.AssemblyQualifiedName;
        }
    }
}