using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Web.Http;

using CMS.Base;
using CMS.EventLog;
using TaskBuilder.Models.Diagram;
using TaskBuilder.Tasks;

namespace TaskBuilder
{
    public class TasksController : ApiController
    {
        [HttpPost]
        [TaskBuilderSecuredActionFilter]
        public IHttpActionResult SaveTask([FromBody] Diagram diagram)
        {
            TaskInfoProvider.SetTaskInfo(diagram);

            return Json(new
            {
                result = "savesuccess"
            });
        }

        [HttpPost]
        [TaskBuilderSecuredActionFilter]
        public void RunTask([FromBody] Diagram diagram)
        {
            var sw1 = new Stopwatch();

            sw1.Start();

            Run(diagram);

            sw1.Stop();

            EventLogProvider.LogInformation(nameof(TasksController), "TESTBENCHMARK",
                $"Log event to Event log: {(double)sw1.ElapsedTicks / Stopwatch.Frequency * 1000L}ms"
                );
        }

        private void Run(Diagram diagram)
        {
            var typeObjects = new Dictionary<Guid, Tuple<Type, object>>();
            var ports = new Dictionary<Guid, Port>();
            var objects = new Dictionary<Guid, Port>();

            Tuple<Type, object> startTypeObject = null;

            foreach (var node in diagram.Nodes)
            {
                var nodeType = ClassHelper.GetAssembly(node.Type.Substring(0, node.Type.IndexOf('.'))).GetType(node.Type);
                var typeObject = FormatterServices.GetUninitializedObject(nodeType);

                typeObjects.Add(node.Id, Tuple.Create(nodeType, typeObject));

                foreach (var port in node.Ports)
                {
                    ports.Add(port.Id, port);
                }

                // Find the start function and save it
                if (node.Type == "TaskBuilder.Functions.StartFunction")
                {
                    startTypeObject = Tuple.Create(nodeType, typeObject);
                }
            }

            foreach (var link in diagram.Links)
            {
                Tuple<Type, object> source;
                Tuple<Type, object> target;
                Port sourcePort;
                Port targetPort;

                typeObjects.TryGetValue(link.Source, out source);
                typeObjects.TryGetValue(link.Target, out target);
                ports.TryGetValue(link.SourcePort, out sourcePort);
                ports.TryGetValue(link.TargetPort, out targetPort);

                switch (link.Type)
                {
                    case "caller":
                        source.Item1.GetProperty(sourcePort.Type).SetValue(
                            source.Item2,
                            target.Item1.GetMethod(targetPort.Type).CreateDelegate(
                                source.Item1.GetProperty(sourcePort.Type).PropertyType,
                                target.Item2)
                        );
                        break;

                    case "default":
                        target.Item1.GetProperty(targetPort.Type).SetValue(
                            target.Item2,
                            source.Item1.GetProperty(sourcePort.Type).GetMethod.CreateDelegate(
                                target.Item1.GetProperty(targetPort.Type).PropertyType,
                                source.Item2)
                        );
                        break;
                }
            }

            // Call the start function
            startTypeObject?.Item1.GetMethod("SourceInReceiver").Invoke(startTypeObject.Item2, null);
        }

        private void TestBenchmark()
        {
            var source1 = new Functions.Types.StartFunction();
            var target1 = new Functions.Types.EventLogFunction();

            //Connect links
            source1.SourceOutSender = target1.TargetInReceiver;

            // Call start node
            source1.SourceInReceiver();

            var sw2 = new Stopwatch();

            sw2.Start();

            Type sourceType2 = ClassHelper.GetClassType("TaskBuilder", "TaskBuilder.Functions.StartFunction");
            Type targetType2 = ClassHelper.GetClassType("TaskBuilder", "TaskBuilder.Functions.EventLogFunction");

            var source2 = FormatterServices.GetUninitializedObject(sourceType2);
            var target2 = FormatterServices.GetUninitializedObject(targetType2);

            sourceType2.GetProperty("SourceOutSender").SetValue(
                source2,
                targetType2.GetMethod("TargetInReceiver").CreateDelegate(
                    sourceType2.GetProperty("SourceOutSender").PropertyType,
                    target2));
            targetType2.GetProperty("TargetInParameter").SetValue(
                target2,

                sourceType2.GetProperty("SourceOutParameter").GetMethod.CreateDelegate(
                    targetType2.GetProperty("TargetInParameter").PropertyType,
                    source2));

            sourceType2.GetMethod("SourceInReceiver").Invoke(source2, null);

            sw2.Stop();

            EventLogProvider.LogInformation(nameof(TasksController), "TESTBENCHMARK",
                $"Elapsed: {(double)sw2.ElapsedTicks / Stopwatch.Frequency * 1000L}ms{Environment.NewLine}"
                );
        }
    }
}