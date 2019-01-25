using System;
using System.Collections.Generic;
using System.Linq;
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
        private IEnumerable<IFunctionModel> _functionModels;

        public FunctionModelService(IFunctionTypeService functionTypeService, IFunctionModelBuilder functionModelBuilder)
        {
            _functionTypeService = functionTypeService;
            _functionModelBuilder = functionModelBuilder;
        }

        public async Task<IEnumerable<IFunctionModel>> AllFunctionModels()
        {
            if (_functionModels == null)
            {
                _functionModels = await RegisterFunctionModels();
            }

            return _functionModels;
        }

        public IEnumerable<string> AuthorizedFunctionIdentifiers(IUserInfo user, SiteInfoIdentifier siteIdentifier)
        {
            if (user.CheckPrivilegeLevel(UserPrivilegeLevelEnum.Admin, siteIdentifier))
            {
                return _functionTypeService.GetFilteredFunctionIdentifiers(t => true);
            }

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

            List<IFunctionModel> functionModels = new List<IFunctionModel>();

            foreach (var pair in functionTypes)
            {
                functionModels.Add(_functionModelBuilder.BuildFunctionModel(pair.Key, pair.Value));
            }

            return functionModels;
        }
    }
}