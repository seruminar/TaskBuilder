using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

using TaskBuilder.Functions;
using TaskBuilder.Models.Diagram;
using TaskBuilder.Models.Function;
using TaskBuilder.Services.Functions;
using TaskBuilder.Services.Inputs;
using TaskBuilder.Tasks;

namespace TaskBuilder.Services.Tasks
{
    internal class TaskCompiler : ITaskCompiler
    {
        private readonly IFunctionDiscoveryService _functionDiscoveryService;
        private readonly IInputValueService _inputValueService;

        public TaskCompiler(IFunctionDiscoveryService functionDiscoveryService, IInputValueService inputValueService)
        {
            _functionDiscoveryService = functionDiscoveryService;
            _inputValueService = inputValueService;
        }

        public Task PrepareTask(Diagram diagram)
        {
            var startFunctionFullName = "TaskBuilder.Functions.Implementations.StartFunction";

            var invokables = new Dictionary<Guid, IInvokable>(diagram.Nodes.Count);
            var dispatchers = new Dictionary<Guid, IDispatcher>();

            var linkedPorts = new Dictionary<Guid, string>();
            var openInputPorts = new Dictionary<Guid, string>();
            var fieldsModels = new Dictionary<Guid, InputFieldsModel>();
            var portFunctionNames = new Dictionary<Guid, string>();
            var portFunctionGuids = new Dictionary<Guid, Guid>();

            IInvokable startInvokable = null;

            var types = diagram.Nodes.ToDictionary(n => n.Id, node =>
            {
                var type = _functionDiscoveryService.GetFunctionType(node.Function.Assembly, node.Function.TypeName);

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

                    if (port.Type.Equals(FunctionHelper.INPUT, StringComparison.OrdinalIgnoreCase))
                    {
                        openInputPorts.Add(port.Id, port.Name);

                        var fieldsModel = node.Function
                            .Inputs
                            .FirstOrDefault(i => i.Name == port.Name)
                            .FieldsModel;

                        fieldsModels.Add(port.Id, fieldsModel);
                        portFunctionNames.Add(port.Id, node.Function.TypeName);
                        portFunctionGuids.Add(port.Id, node.Id);
                    }
                }

                // Find the start function and save it
                if (node.Function.TypeName == startFunctionFullName)
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
                    InputFieldsModel fields;
                    object value;

                    Type parentType = types[portFunctionGuids[openPort.Key]];
                    IInvokable parent = invokables[portFunctionGuids[openPort.Key]];

                    if (fieldsModels.TryGetValue(openPort.Key, out fields))
                    {
                        value = _inputValueService.ConstructValue(portFunctionNames[openPort.Key], openPort.Value, fields);
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
    }
}