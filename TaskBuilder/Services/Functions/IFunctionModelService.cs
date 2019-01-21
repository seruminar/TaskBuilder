using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CMS;
using CMS.Base;
using CMS.DataEngine;

using TaskBuilder.Functions;
using TaskBuilder.Models.Function;
using TaskBuilder.Services.Functions;

[assembly: RegisterImplementation(typeof(IFunctionModelService), typeof(FunctionModelService), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services.Functions
{
    public interface IFunctionModelService
    {
        /// <summary>
        /// Finds all of the function types, gets their ports and creates a model in the cache.
        /// When a task builder is opened, the React app pulls in the models for deserialization and creating new ones in the side drawer.
        /// </summary>
        Task<IEnumerable<FunctionModel>> AllFunctionModels();

        /// <summary>
        /// Get all <see cref="IFunctionModel"/>s authorized for given user and site.
        /// </summary>
        IEnumerable<Guid> AuthorizedFunctionIdentifiers(IUserInfo user, SiteInfoIdentifier siteIdentifier);

        bool CompareFunctionAndType(FunctionInfo functionInfo, Type functionType);
    }
}