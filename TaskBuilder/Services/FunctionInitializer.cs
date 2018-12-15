using System;
using System.Text.RegularExpressions;
using System.Web.Caching;

using CMS.Base;
using CMS.Helpers;

using TaskBuilder.Services;

namespace TaskBuilder
{
    internal class FunctionInitializer : AbstractWorker
    {
        private readonly IFunctionModelService _functionModelService;

        public FunctionInitializer(IFunctionModelService functionModelService)
        {
            _functionModelService = functionModelService;
        }

        public override void Run()
        {
            CacheHelper.Add(
                    TaskBuilderHelper.CACHE_REGISTER_KEY,
                    _functionModelService.RegisterFunctionModels(),
                    null,
                    Cache.NoAbsoluteExpiration,
                    TimeSpan.FromMinutes(TaskBuilderHelper.CACHE_MINUTES)
                );
        }
    }
}