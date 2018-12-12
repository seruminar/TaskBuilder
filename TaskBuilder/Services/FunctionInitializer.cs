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

            foreach (var type in _functionModelService.DiscoverFunctionTypes())
            {
                var name = type.Name;

                // Remove trailing "Function" for readability
                if (name.EndsWith("Function"))
                {
                    name = name.Substring(0, name.Length - 8);
                }

                var function = new FunctionInfo()
                {
                    FunctionDisplayName = Regex.Replace(name, "(\\B[A-Z])", " $1"),
                    FunctionName = name
                };

                FunctionInfoProvider.SetFunctionInfo(function);
            }
        }
    }
}