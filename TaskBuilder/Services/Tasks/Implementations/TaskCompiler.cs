using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

using TaskBuilder.Functions;
using TaskBuilder.Models.Function.InputValue;
using TaskBuilder.Models.Graph;
using TaskBuilder.Services.Functions;
using TaskBuilder.Services.Inputs;
using TaskBuilder.Tasks;

namespace TaskBuilder.Services.Tasks
{
    internal class TaskCompiler : ITaskCompiler
    {
        private readonly IFunctionTypeService _functionDiscoveryService;
        private readonly IInputValueService _inputValueService;

        public TaskCompiler(IFunctionTypeService functionDiscoveryService, IInputValueService inputValueService)
        {
            _functionDiscoveryService = functionDiscoveryService;
            _inputValueService = inputValueService;
        }

        public Task PrepareTask(Graph diagram)
        {
            var startFunctionTypeGuid = Guid.Parse("DBB090FA-E415-4A8A-AFAB-52304C5B8122");

            var invokables = new Dictionary<Guid, IInvokable>(diagram.Nodes.Count());
            var dispatchers = new Dictionary<Guid, IDispatcher>();
            var dispatcher2s = new Dictionary<Guid, IDispatcher2>();

            var linkedPortIdPortName = new Dictionary<Guid, string>();
            var openPortIdPortName = new Dictionary<Guid, string>();

            var portIdFieldsModel = new Dictionary<Guid, IInputValueModel>();
            var portIdNodeId = new Dictionary<Guid, Guid>();
            var portIdFunctionTypeGuid = new Dictionary<Guid, Guid>();

            IInvokable startInvokable = null;

            var nodeIdFunctionType = diagram.Nodes.ToDictionary(n => n.Id, node =>
            {
                var type = _functionDiscoveryService.GetFunctionType(node.FunctionTypeGuid);

                var function = FormatterServices.GetUninitializedObject(type);

                invokables.Add(node.Id, function as IInvokable);

                if (function is IDispatcher)
                {
                    dispatchers.Add(node.Id, function as IDispatcher);
                }

                if (function is IDispatcher2)
                {
                    dispatcher2s.Add(node.Id, function as IDispatcher2);
                }

                foreach (var port in node.Ports)
                {
                    if (port.Links.Any())
                    {
                        linkedPortIdPortName.Add(port.Id, port.Name);
                        continue;
                    }

                    if (port.Type == PortType.Input)
                    {
                        openPortIdPortName.Add(port.Id, port.Name);

                        var fieldsModel = diagram.InputValues[port.Id];

                        portIdFieldsModel.Add(port.Id, fieldsModel);
                        portIdNodeId.Add(port.Id, node.Id);
                        portIdFunctionTypeGuid.Add(port.Id, node.FunctionTypeGuid);
                    }
                }

                // Find the start function and save it
                if (node.FunctionTypeGuid == startFunctionTypeGuid)
                {
                    startInvokable = function as IInvokable;
                }

                return type;
            });

            foreach (var link in diagram.Links)
            {
                IInvokable target = invokables[link.Target];
                string sourcePort = linkedPortIdPortName[link.SourcePort];
                string targetPort = linkedPortIdPortName[link.TargetPort];

                switch (link.Type)
                {
                    case LinkType.Dispatch:
                        dispatchers[link.Source].Dispatch = target.Invoke;
                        break;

                    case LinkType.Dispatch2:
                        dispatcher2s[link.Source].Dispatch2 = target.Invoke;
                        break;

                    case LinkType.Parameter:

                        Type sourceType = nodeIdFunctionType[link.Source];
                        Type targetType = nodeIdFunctionType[link.Target];
                        var source = invokables[link.Source];

                        targetType.GetProperty(targetPort).SetValue(
                            target,
                            sourceType.GetProperty(sourcePort).GetMethod.CreateDelegate(
                                targetType.GetProperty(targetPort).PropertyType,
                                source)
                        );
                        break;
                }
            }

            if (openPortIdPortName.Any())
            {
                foreach (var openPort in openPortIdPortName)
                {
                    IInputValueModel fields;
                    object value;

                    Guid nodeId = portIdNodeId[openPort.Key];
                    Type parentType = nodeIdFunctionType[nodeId];
                    IInvokable parent = invokables[nodeId];

                    if (portIdFieldsModel.TryGetValue(openPort.Key, out fields))
                    {
                        value = _inputValueService.BuildValue(portIdFunctionTypeGuid[openPort.Key], openPort.Value, fields);
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