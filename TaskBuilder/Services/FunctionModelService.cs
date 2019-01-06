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
using TaskBuilder.Models.Function;

namespace TaskBuilder.Services
{
    internal class FunctionModelService : IFunctionModelService
    {
        #region Public Properties

        public IEnumerable<IFunctionModel> FunctionModels { get; } = CacheHelper.Cache(RegisterFunctionModels, new CacheSettings(TaskBuilderHelper.CACHE_MINUTES, TaskBuilderHelper.CACHE_REGISTER_KEY));

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Get all <see cref="IBaseModel"/>s authorized for given user and site.
        /// </summary>
        public IEnumerable<IFunctionModel> GetAuthorizedFunctionModels(IUserInfo user, SiteInfoIdentifier siteIdentifier)
        {
            var functionClassNames = FunctionInfoProvider
                                        .GetFunctionsForUserAndSite(user, siteIdentifier)
                                        .AsSingleColumn("FunctionClass")
                                        .GetListResult<string>()
                                        .Select(className => new FunctionModel(className));

            return FunctionModels.Intersect(functionClassNames, new FunctionModelComparer());
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Finds all of the function types, gets their ports and creates a model in the cache.
        /// When a task builder is opened, the React app pulls in the models for deserialization and creating new ones in the side drawer.
        /// </summary>
        private static IEnumerable<IFunctionModel> RegisterFunctionModels()
        {
            var functionTypes = DiscoverFunctionTypes();

            if (!functionTypes.Any())
            {
                throw new Exception($"{nameof(FunctionInitializer)} could not find any function types.");
            }

            return functionTypes.Select(t => new FunctionModel(t));
        }

        /// <summary>
        /// Finds all of the loaded types that use <see cref="FunctionAttribute"/>.
        /// </summary>
        private static IEnumerable<Type> DiscoverFunctionTypes()
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

        #endregion Private Methods
    }
}