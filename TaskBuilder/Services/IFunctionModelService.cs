using System.Collections.Generic;

using CMS;
using CMS.Base;
using CMS.DataEngine;
using TaskBuilder.Models;
using TaskBuilder.Services;

[assembly: RegisterImplementation(typeof(IFunctionModelService), typeof(FunctionModelService), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services
{
    public interface IFunctionModelService
    {
        IEnumerable<IFunctionModel> AllFunctionModels { get; }

        IEnumerable<IFunctionModel> GetAuthorizedFunctionModels(IUserInfo user, SiteInfoIdentifier siteIdentifier);

        IFunctionModel GetFunctionModel(string functionName);

        IEnumerable<IFunctionModel> RegisterFunctionModels();
    }
}