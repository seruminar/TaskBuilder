using System.Web.Http;

using CMS;
using CMS.Core;
using CMS.DataEngine;

using TaskBuilder.Services;

[assembly: RegisterModule(typeof(TaskBuilder.TaskBuilder))]

namespace TaskBuilder
{
    internal class TaskBuilder : Module
    {
        public TaskBuilder() : base(nameof(TaskBuilder))
        {
        }

        protected override void OnInit()
        {
            base.OnInit();

            InitializeFunctions();

            GlobalConfiguration.Configuration.Routes.MapHttpRoute("taskbuilder", "taskbuilder/{controller}/{id}", new { id = RouteParameter.Optional });
        }

        private void InitializeFunctions()
        {
            var initializer = new FunctionInitializer(Service.Resolve<IFunctionModelService>());
            initializer.RunAsync();
        }
    }
}