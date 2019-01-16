using System;
using System.Collections.Generic;
using System.Linq;

using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;
using TaskBuilder.Functions;
using TaskBuilder.Models.Function;

namespace TaskBuilder.Services
{
    public class FunctionModelService : IFunctionModelService
    {
        private readonly IFunctionDiscoveryService _functionDiscoveryService;
        private readonly IFunctionModelBuilder _functionModelBuilder;

        public FunctionModelService(IFunctionDiscoveryService functionDiscoveryService, IFunctionModelBuilder functionModelBuilder)
        {
            _functionDiscoveryService = functionDiscoveryService;
            _functionModelBuilder = functionModelBuilder;
        }

        public IEnumerable<IFunctionModel> GetFunctionModels()
        {
            return CacheHelper.Cache(
                RegisterFunctionModels,
                new CacheSettings(
                    TaskBuilderHelper.CACHE_MINUTES,
                    TaskBuilderHelper.CACHE_REGISTER_KEY
                    )
                );
        }

        public IEnumerable<IFunctionModel> GetAuthorizedFunctionModels(IUserInfo user, SiteInfoIdentifier siteIdentifier)
        {
            var functionClassNames = FunctionInfoProvider
                                        .GetFunctionsForUserAndSite(user, siteIdentifier)
                                        .AsSingleColumn(nameof(FunctionInfo.FunctionClass))
                                        .GetListResult<string>()
                                        .Select(className => _functionModelBuilder.BuildSimpleFunctionModel(className));

            return GetFunctionModels().Intersect(functionClassNames, new FunctionModelComparer());
        }

        private IEnumerable<IFunctionModel> RegisterFunctionModels()
        {
            var functionTypes = _functionDiscoveryService.DiscoverFunctionTypes();

            if (!functionTypes.Any())
            {
                throw new Exception($"{nameof(FunctionModelService)} could not find any function types.");
            }

            return functionTypes.Select(_functionModelBuilder.BuildFunctionModel);
        }
    }
}