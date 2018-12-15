using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using CMS.Core;
using CMS.Helpers;
using TaskBuilder.Attributes;
using TaskBuilder.Functions;

namespace TaskBuilder.Services
{
    internal class FunctionModelService : IFunctionModelService
    {
        public IEnumerable<FunctionModel> FunctionModels => CacheHelper.Cache(RegisterFunctionModels, new CacheSettings(TaskBuilderHelper.CACHE_MINUTES, TaskBuilderHelper.CACHE_REGISTER_KEY));

        public FunctionModel GetFunctionModel(string functionName) => FunctionModels.FirstOrDefault(m => m.Name == functionName);

        /// <summary>
        /// Finds all of the function types, gets their ports and creates a model in the cache.
        /// When a task builder is opened, the React app pulls in the models for deserialization and creating new ones in the side drawer.
        /// </summary>
        public HashSet<FunctionModel> RegisterFunctionModels()
        {
            var functionTypes = DiscoverFunctionTypes();

            if (!functionTypes.Any())
            {
                throw new Exception($"{nameof(FunctionInitializer)} could not find any functions.");
            }

            return new HashSet<FunctionModel>(functionTypes.Select(t => new FunctionModel(t)));
        }

        /// <summary>
        /// Finds all of the function types that extend <see cref="Function"/>.
        /// </summary>
        private List<Type> DiscoverFunctionTypes()
        {
            // Get loaded assemblies
            var discoveredAssemblies = AssemblyDiscoveryHelper.GetAssemblies(false);

            var functionTypes = new List<Type>();

            if (discoveredAssemblies.Any())
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

                    foreach (Type type in assemblyClassTypes)
                    {
                        if (Attribute.IsDefined(type, typeof(FunctionAttribute)))
                            functionTypes.Add(type);
                    }
                }
            }

            return functionTypes;
        }
    }
}