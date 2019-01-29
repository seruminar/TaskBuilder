using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Base;
using CMS.DataEngine;

using TaskBuilder.Functions;
using TaskBuilder.Models.Function;

namespace TaskBuilder.Services.Functions
{
    public class FunctionModelService : IFunctionModelService
    {
        private readonly IFunctionTypeService _functionTypeService;
        private readonly IFunctionModelBuilder _functionModelBuilder;
        private IEnumerable<IFunctionModel> _allFunctionModels;

        public FunctionModelService(IFunctionTypeService functionTypeService, IFunctionModelBuilder functionModelBuilder)
        {
            _functionTypeService = functionTypeService;
            _functionModelBuilder = functionModelBuilder;
        }

        public IEnumerable<IFunctionModel> AllFunctionModels => _allFunctionModels ?? RegisterFunctionModels().Result;

        public async Task<IEnumerable<IFunctionModel>> RegisterFunctionModels()
        {
            var functionTypes = await _functionTypeService.DiscoverFunctionTypes();

            List<IFunctionModel> functionModels = new List<IFunctionModel>();

            foreach (var guidType in functionTypes)
            {
                functionModels.Add(
                    _functionModelBuilder.BuildFunctionModel(guidType.Value, guidType.Key)
                );
            }

            _allFunctionModels = functionModels;

            return _allFunctionModels;
        }

        public IEnumerable<Guid> AuthorizedFunctionGuids(IUserInfo user, SiteInfoIdentifier siteIdentifier)
        {
            if (user.CheckPrivilegeLevel(UserPrivilegeLevelEnum.Admin, siteIdentifier))
            {
                return _functionTypeService.GetFunctionGuids(g => true);
            }

            return FunctionInfoProvider
                    .GetFunctionsForUserAndSite(user, siteIdentifier)
                    .AsSingleColumn(nameof(FunctionInfo.FunctionTypeGuid))
                    .GetListResult<Guid>();
        }
    }
}