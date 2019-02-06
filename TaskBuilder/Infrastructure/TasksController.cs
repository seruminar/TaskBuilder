using System;
using System.Diagnostics;
using System.Web.Http;

using CMS.Core;
using CMS.EventLog;
using CMS.Helpers;

using TaskBuilder.Models.Graph;
using TaskBuilder.Services.Tasks;
using TaskBuilder.Tasks;

namespace TaskBuilder.Infrastructure
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
        public IHttpActionResult SaveTask([FromBody] Graph diagram)
        {
            try
            {
                TaskInfoProvider.SetTaskInfo(diagram);
            }
            catch (Exception ex)
            {
                EventLogProvider.LogException(nameof(TasksController), nameof(SaveTask).ToUpper(), ex);

                return JsonResult(TasksControllerResult.Error, "taskbuilder.messages.error");
            }

            return JsonResult(TasksControllerResult.Success, "taskbuilder.messages.savesuccessful");
        }

        [HttpPost]
        [TaskBuilderSecuredActionFilter]
        public IHttpActionResult RunTask([FromBody] Graph diagram)
        {
            try
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
            }
            catch (Exception ex)
            {
                EventLogProvider.LogException(nameof(TasksController), nameof(RunTask).ToUpper(), ex);

                return JsonResult(TasksControllerResult.Error, "taskbuilder.messages.error");
            }

            return JsonResult(TasksControllerResult.Success, "taskbuilder.messages.runcompleted");
        }

        private IHttpActionResult JsonResult(TasksControllerResult result, string messageResourceString)
        {
            return Json(new
            {
                result,
                message = ResHelper.GetString(messageResourceString)
            },
            TaskBuilderHelper.JsonSerializerSettings);
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