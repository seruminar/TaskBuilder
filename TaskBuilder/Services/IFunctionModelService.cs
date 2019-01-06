using System.Collections.Generic;

using CMS;
using CMS.Base;
using CMS.DataEngine;
using TaskBuilder.Models.Function;
using TaskBuilder.Services;

[assembly: RegisterImplementation(typeof(IFunctionModelService), typeof(FunctionModelService), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services
{
    public interface IFunctionModelService
    {
        IEnumerable<IFunctionModel> FunctionModels { get; }

        IEnumerable<IFunctionModel> GetAuthorizedFunctionModels(IUserInfo user, SiteInfoIdentifier siteIdentifier);
    }
}