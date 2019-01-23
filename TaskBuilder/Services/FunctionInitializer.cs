using CMS.Base;

using TaskBuilder.Services.Functions;

namespace TaskBuilder.Services
{
    internal class FunctionInitializer : AbstractWorker
    {
        private readonly IFunctionModelService _functionModelService;

        public FunctionInitializer(IFunctionModelService functionModelService)
        {
            _functionModelService = functionModelService;
        }

        public override async void Run()
        {
            var discard = await _functionModelService.AllFunctionModels();
        }
    }
}