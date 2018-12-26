using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;
using CMS;
using CMS.Core;
using CMS.DataEngine;
using Newtonsoft.Json;
using TaskBuilder.Models;
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

            TaskInfo.TYPEINFO.Events.Insert.Before += HandleImportTask;

            // Map route directly to RouteTable to enable session access
            RouteTable.Routes.MapHttpRoute("taskbuilder", "taskbuilder/{controller}/{action}").RouteHandler = new SessionRouteHandler();
        }

        private void HandleImportTask(object sender, ObjectEventArgs e)
        {
            if (e.Object is TaskInfo task && !string.IsNullOrEmpty(task.TaskGraph))
            {
                var taskGraph = JsonConvert.DeserializeObject<DiagramModel>(task.TaskGraph);

                taskGraph.Id = task.TaskGuid;
                task.TaskGraph = JsonConvert.SerializeObject(taskGraph);
            }
        }

        private void InitializeFunctions()
        {
            var initializer = new FunctionInitializer(Service.Resolve<IFunctionModelService>());
            initializer.RunAsync();
        }

        public class SessionRouteHandler : IRouteHandler
        {
            public IHttpHandler GetHttpHandler(RequestContext requestContext)
            {
                return new SessionControllerHandler(requestContext.RouteData);
            }
        }

        public class SessionControllerHandler : HttpControllerHandler, IRequiresSessionState
        {
            public SessionControllerHandler(RouteData routeData)
                : base(routeData)
            { }
        }
    }
}