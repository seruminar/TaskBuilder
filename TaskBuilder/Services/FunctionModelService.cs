using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CMS.Base;
using CMS.Core;
using CMS.DataEngine;
using CMS.Helpers;

using TaskBuilder.Attributes;
using TaskBuilder.Functions;
using TaskBuilder.Models;

namespace TaskBuilder.Services
{
    internal class FunctionModelService : IFunctionModelService
    {
        private IEnumerable<ITypedModel> FunctionModels => CacheHelper.Cache(RegisterFunctionModels, new CacheSettings(TaskBuilderHelper.CACHE_MINUTES, TaskBuilderHelper.CACHE_REGISTER_KEY));

        public IEnumerable<ITypedModel> AllFunctionModels => FunctionModels;

        /// <summary>
        /// Finds all of the function types, gets their ports and creates a model in the cache.
        /// When a task builder is opened, the React app pulls in the models for deserialization and creating new ones in the side drawer.
        /// </summary>
        public IEnumerable<ITypedModel> RegisterFunctionModels()
        {
            var functionTypes = DiscoverFunctionTypes();

            if (!functionTypes.Any())
            {
                throw new Exception($"{nameof(FunctionInitializer)} could not find any function types.");
            }

            return functionTypes.Select(t => new FunctionModel(t));
        }

        /// <summary>
        /// Get a <see cref="ITypedModel"/> by name.
        /// </summary>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public ITypedModel GetFunctionModel(string functionName) => FunctionModels.FirstOrDefault(m => m.Name == functionName);

        /// <summary>
        /// Get all <see cref="ITypedModel"/>s authorized for given user and site.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="siteIdentifier"></param>
        /// <returns></returns>
        public IEnumerable<ITypedModel> GetAuthorizedFunctionModels(IUserInfo user, SiteInfoIdentifier siteIdentifier)
        {
            var functionClassNames = FunctionInfoProvider
                                        .GetFunctionsForUserAndSite(user, siteIdentifier)
                                        .AsSingleColumn("FunctionClass")
                                        .GetListResult<string>()
                                        .Select(className => new TypedModel(className));

            return FunctionModels.Intersect(functionClassNames, new FunctionModelComparer());
        }

        /// <summary>
        /// Finds all of the loaded types that use <see cref="FunctionAttribute"/>.
        /// </summary>
        private IEnumerable<Type> DiscoverFunctionTypes()
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

                    foreach (Type type in assemblyClassTypes.Where(x => x != null))
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