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
        /// <summary>
        /// Finds all of the function types, gets their ports and creates a model in the cache.
        /// When a task builder is opened, the React app pulls in the models for deserialization and creating new ones in the side drawer.
        /// </summary>
        IEnumerable<IFunctionModel> FunctionModels { get; }

        /// <summary>
        /// Get all <see cref="IBaseModel"/>s authorized for given user and site.
        /// </summary>
        IEnumerable<IFunctionModel> GetAuthorizedFunctionModels(IUserInfo user, SiteInfoIdentifier siteIdentifier);
    }
}