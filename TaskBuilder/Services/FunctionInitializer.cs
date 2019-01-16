using CMS.Base;

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
            _functionModelService.GetFunctionModels();
        }
    }
}