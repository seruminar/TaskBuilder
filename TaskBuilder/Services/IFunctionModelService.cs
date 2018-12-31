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
        IEnumerable<ITypedModel> AllFunctionModels { get; }

        IEnumerable<ITypedModel> GetAuthorizedFunctionModels(IUserInfo user, SiteInfoIdentifier siteIdentifier);

        ITypedModel GetFunctionModel(string functionName);

        IEnumerable<ITypedModel> RegisterFunctionModels();
    }
}