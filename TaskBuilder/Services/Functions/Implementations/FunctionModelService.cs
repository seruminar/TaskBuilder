using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;

using TaskBuilder.Functions;
using TaskBuilder.Models.Function;

namespace TaskBuilder.Services.Functions
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

        public async Task<IEnumerable<FunctionModel>> AllFunctionModels()
        {
            return await CacheHelper.Cache(
                RegisterFunctionModels,
                new CacheSettings(
                    TaskBuilderHelper.CACHE_MINUTES,
                    TaskBuilderHelper.CACHE_REGISTER_KEY
                    )
                );
        }

        public IEnumerable<FunctionModel> AuthorizedFunctionModels(IUserInfo user, SiteInfoIdentifier siteIdentifier)
        {
            var functionClassNames = FunctionInfoProvider
                                        .GetFunctionsForUserAndSite(user, siteIdentifier)
                                        .Columns(nameof(FunctionInfo.FunctionClass), nameof(FunctionInfo.FunctionAssembly))
                                        .Select(r => _functionModelBuilder.BuildSimpleFunctionModel(
                                                            r[nameof(FunctionInfo.FunctionClass)].ToString(),
                                                            r[nameof(FunctionInfo.FunctionAssembly)].ToString()
                                                        )
                                                );

            return functionClassNames;
        }

        private async Task<IEnumerable<FunctionModel>> RegisterFunctionModels()
        {
            var functionTypes = await _functionDiscoveryService.DiscoverFunctionTypes();

            if (!functionTypes.Any())
            {
                throw new Exception($"{nameof(FunctionModelService)} could not find any function types.");
            }

            return functionTypes.Select(_functionModelBuilder.BuildFunctionModel);
        }
    }
}