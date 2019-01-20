using System;
using System.Diagnostics;
using System.Web.Http;

using CMS.Core;
using CMS.EventLog;
using CMS.Helpers;

using TaskBuilder.Models.Diagram;
using TaskBuilder.Services.Tasks;
using TaskBuilder.Tasks;

namespace TaskBuilder
{
    public class TasksController : ApiController
    {
        private readonly ITaskCompiler _taskCompiler;
        private readonly ITaskRunner _taskRunner;

        public TasksController()
        {
            _taskCompiler = Service.Resolve<ITaskCompiler>();
            _taskRunner = Service.Resolve<ITaskRunner>();
        }

        [HttpPost]
        [TaskBuilderSecuredActionFilter]
        public IHttpActionResult SaveTask([FromBody] Diagram diagram)
        {
            TaskInfoProvider.SetTaskInfo(diagram);

            return Json(new
            {
                result = TasksControllerResult.Success,
                message = ResHelper.GetString("taskbuilder.messages.savesuccessful")
            });
        }

        [HttpPost]
        [TaskBuilderSecuredActionFilter]
        public IHttpActionResult RunTask([FromBody] Diagram diagram)
        {
            var sw1 = new Stopwatch();
            long afterCompile;
            sw1.Start();

            var task = _taskCompiler.PrepareTask(diagram);
            afterCompile = sw1.ElapsedTicks;

            sw1.Restart();
            _taskRunner.RunTask(task, true);

            sw1.Stop();

            EventLogProvider.LogInformation(nameof(TasksController), "TESTBENCHMARK",
                $@"{TaskInfoProvider.GetTaskInfo(diagram.Id)?.TaskDisplayName}:{Environment.NewLine}
Prepare: {(double)afterCompile / Stopwatch.Frequency * 1000L}ms{Environment.NewLine}
Run: {(double)sw1.ElapsedTicks / Stopwatch.Frequency * 1000L}ms"
                );

            return Json(new
            {
                result = TasksControllerResult.Success,
                message = ResHelper.GetString("taskbuilder.messages.runcompleted")
            });
        }

        private void TestBenchmark()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            // Some code

            stopwatch.Stop();

            EventLogProvider.LogInformation(nameof(TasksController), "TESTBENCHMARK",
                $"Elapsed: {(double)stopwatch.ElapsedTicks / Stopwatch.Frequency * 1000L}ms{Environment.NewLine}"
                );
        }
    }
}