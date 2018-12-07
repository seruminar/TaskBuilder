using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Web.Http;

using CMS.Base;
using CMS.Core;
using CMS.EventLog;

using TaskBuilder.Actions;
using TaskBuilder.Models;

namespace TaskBuilder
{
    public class RunTaskController : ApiController
    {
        public object EventlogProvider { get; private set; }

        public void Post([FromBody] DiagramModel diagram)
        {
            TestBenchmark();

            // POC approach
            //var listOfNodes = new List<TaskAction>();

            //foreach (var node in diagram.Nodes)
            //{
            //    TaskAction createdNode = null;
            //    switch (node.Type)
            //    {
            //        case "startNode":
            //            createdNode = new StartAction(node);
            //            break;

            //        case "eventLogNode":
            //            createdNode = new EventLogAction(node);
            //            break;

            //        default:
            //            break;
            //    }

            //    listOfNodes.Add(createdNode);
            //}

            //foreach (var node in listOfNodes)
            //{
            //    var link = diagram.Links.FirstOrDefault(l => l.Source == node.Guid);

            //    if (link != null)
            //    {
            //        var targetNode = listOfNodes.FirstOrDefault(n => n.Guid == link.Target);

            //        if (targetNode != null)
            //        {
            //            node.Targets.Add(targetNode);
            //        }
            //    }
            //}

            //var startNode = listOfNodes.FirstOrDefault(n => n is StartAction);

            //startNode?.Execute(null);
        }

        private void TestBenchmark()
        {
            var sw1 = new Stopwatch();
            var sw2 = new Stopwatch();

            sw1.Start();

            //// Testing (basic approach)
            //var source1 = new StartAction(new Node());
            //var target1 = new EventLogAction(new Node());

            // Connect links
            //source1.SourceOutSender = target1.TargetInReceiver;
            //target1.TargetInParameter = source1.SourceOutParameter;

            //// Call start node
            //source1.SourceInReceiver();

            sw1.Stop();

            sw2.Start();

            Type sourceType2 = ClassHelper.GetClassType("TaskBuilder", "TaskBuilder.Actions.StartAction");
            Type targetType2 = ClassHelper.GetClassType("TaskBuilder", "TaskBuilder.Actions.EventLogAction");

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




            EventLogProvider.LogInformation(nameof(RunTaskController), "TESTBENCHMARK",
                $@"Direct instantiation: {(double)sw1.ElapsedTicks / Stopwatch.Frequency * 1000L}ms{Environment.NewLine}
                FormatterServices: {(double)sw2.ElapsedTicks / Stopwatch.Frequency * 1000L}ms{Environment.NewLine}"
                );
        }

        private static object Formatter(string assemblyName, string className)
        {

            //if (HasDefaultConstructor(t))
            //    return Expression.Lambda<Func<object>>(Expression.New(t)).Compile().Invoke();

            //return FormatterServices.GetUninitializedObject(t);
            return new object();
        }

        private static bool HasDefaultConstructor(Type t)
        {
            return t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}