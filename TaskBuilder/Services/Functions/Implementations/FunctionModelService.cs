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
        private readonly IFunctionTypeService _functionTypeService;
        private readonly IFunctionModelBuilder _functionModelBuilder;

        public FunctionModelService(IFunctionTypeService functionTypeService, IFunctionModelBuilder functionModelBuilder)
        {
            _functionTypeService = functionTypeService;
            _functionModelBuilder = functionModelBuilder;
        }

        public async Task<IEnumerable<IFunctionModel>> AllFunctionModels()
        {
            return await CacheHelper.Cache(
                RegisterFunctionModels,
                new CacheSettings(
                    TaskBuilderHelper.CACHE_MINUTES,
                    TaskBuilderHelper.CACHE_REGISTER_KEY
                    )
                );
        }

        public IEnumerable<string> AuthorizedFunctionIdentifiers(IUserInfo user, SiteInfoIdentifier siteIdentifier)
        {
            var functionClassNames = FunctionInfoProvider
                                        .GetFunctionsForUserAndSite(user, siteIdentifier)
                                        .Columns(nameof(FunctionInfo.FunctionClass), nameof(FunctionInfo.FunctionAssembly));

            return _functionTypeService
                .GetFilteredFunctionIdentifiers(t => functionClassNames
                                                        .FirstOrDefault(f => CompareFunctionAndType(f, t)) != null
                                                );
        }

        public bool CompareFunctionAndType(FunctionInfo functionInfo, Type functionType)
        {
            return functionInfo.FunctionClass == functionType.FullName
                && functionInfo.FunctionAssembly == functionType.Assembly.GetName().Name;
        }

        private async Task<IEnumerable<IFunctionModel>> RegisterFunctionModels()
        {
            var functionTypes = await _functionTypeService.DiscoverFunctionTypes();

            if (!functionTypes.Any())
            {
                throw new Exception($"{nameof(FunctionModelService)} could not find any function types.");
            }

            return functionTypes.Select(pair => _functionModelBuilder.BuildFunctionModel(pair.Key, pair.Value));
        }
    }
}