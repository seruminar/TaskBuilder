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
using TaskBuilder.Models.Function;

namespace TaskBuilder.Services
{
    internal class FunctionModelService : IFunctionModelService
    {
        #region Public Properties

        public IEnumerable<IFunctionModel> FunctionModels { get; } = CacheHelper.Cache(RegisterFunctionModels, new CacheSettings(TaskBuilderHelper.CACHE_MINUTES, TaskBuilderHelper.CACHE_REGISTER_KEY));

        #endregion Public Properties

        #region Public Methods

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