using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Web.Http;

using CMS.Core;
using CMS.EventLog;
using CMS.Helpers;

using TaskBuilder.Functions;
using TaskBuilder.Models.Diagram;
using TaskBuilder.Services;
using TaskBuilder.Tasks;

namespace TaskBuilder
{
    public class TasksController : ApiController
    {
        private readonly IFunctionDiscoveryService _functionDiscoveryService;
        private readonly IInputValueService _inputValueService;

        public TasksController()
        {
            _functionDiscoveryService = Service.Resolve<IFunctionDiscoveryService>();
            _inputValueService = Service.Resolve<IInputValueService>();
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
            long afterPrepare;
            sw1.Start();

            var task = PrepareTask(diagram);
            afterPrepare = sw1.ElapsedTicks;

            sw1.Restart();
            task.Invoke();

            sw1.Stop();

            EventLogProvider.LogInformation(nameof(TasksController), "TESTBENCHMARK",
                $@"{TaskInfoProvider.GetTaskInfo(diagram.Id).TaskDisplayName}:{Environment.NewLine}
Prepare: {(double)afterPrepare / Stopwatch.Frequency * 1000L}ms{Environment.NewLine}
Run: {(double)sw1.ElapsedTicks / Stopwatch.Frequency * 1000L}ms"
                );

            return Json(new
            {
                result = TasksControllerResult.Success,
                message = ResHelper.GetString("taskbuilder.messages.runcompleted")
            });
        }

        private Task PrepareTask(Diagram diagram, string startFunctionFullName = "TaskBuilder.Functions.Implementations.StartFunction")
        {
            var invokables = new Dictionary<Guid, IInvokable>(diagram.Nodes.Count);
            var dispatchers = new Dictionary<Guid, IDispatcher>();

            var linkedPorts = new Dictionary<Guid, string>();
            var openInputPorts = new Dictionary<Guid, string>();
            var portValues = new Dictionary<Guid, string>();
            var portFunctionNames = new Dictionary<Guid, string>();
            var portFunctionGuids = new Dictionary<Guid, Guid>();

            IInvokable startInvokable = null;

            var types = diagram.Nodes.ToDictionary(n => n.Id, node =>
            {
                var type = _functionDiscoveryService.GetFunctionType(node.Type);

                var function = FormatterServices.GetUninitializedObject(type);

                invokables.Add(node.Id, function as IInvokable);

                if (function is IDispatcher)
                {
                    dispatchers.Add(node.Id, function as IDispatcher);
                }

                foreach (var port in node.Ports)
                {
                    if (port.Linked)
                    {
                        linkedPorts.Add(port.Id, port.Name);
                        continue;
                    }

                    if (port.Type.Equals(nameof(Input), StringComparison.OrdinalIgnoreCase))
                    {
                        openInputPorts.Add(port.Id, port.Name);

                        if (!string.IsNullOrEmpty(port.Value))
                        {
                            portValues.Add(port.Id, port.Value);
                            portFunctionNames.Add(port.Id, node.Type);
                        }

                        portFunctionGuids.Add(port.Id, node.Id);
                    }
                }

                // Find the start function and save it
                if (node.Type == startFunctionFullName)
                {
                    startInvokable = function as IInvokable;
                }

                return type;
            });

            foreach (var link in diagram.Links)
            {
                IDispatcher source = dispatchers[link.Source];
                IInvokable target = invokables[link.Target];
                string sourcePort = linkedPorts[link.SourcePort];
                string targetPort = linkedPorts[link.TargetPort];

                switch (link.Type)
                {
                    case "caller":
                        source.Dispatch = target.Invoke;
                        break;

                    case "parameter":

                        Type sourceType = types[link.Source];
                        Type targetType = types[link.Target];

                        targetType.GetProperty(targetPort).SetValue(
                            target,
                            sourceType.GetProperty(sourcePort).GetMethod.CreateDelegate(
                                targetType.GetProperty(targetPort).PropertyType,
                                source)
                        );
                        break;
                }
            }

            if (openInputPorts.Any())
            {
                foreach (var openPort in openInputPorts)
                {
                    string valueData;
                    object value;

                    Type parentType = types[portFunctionGuids[openPort.Key]];
                    IInvokable parent = invokables[portFunctionGuids[openPort.Key]];

                    if (portValues.TryGetValue(openPort.Key, out valueData))
                    {
                        value = _inputValueService.ConstructValue(portFunctionNames[openPort.Key], openPort.Value, valueData);
                    }
                    else
                    {
                        value = null;
                    }

                    parentType.GetProperty(openPort.Value).SetValue(
                        parent,
                        Expression.Lambda(
                            Expression.Convert(
                                Expression.Constant(value),
                                parentType.GetProperty(openPort.Value).PropertyType.GenericTypeArguments[0]
                            )
                        ).Compile()
                    );
                }
            }

            return new Task(invokables, startInvokable);
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