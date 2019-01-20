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

        public override void Run()
        {
            _functionModelService.AllFunctionModels();
        }
    }
}