using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TaskBuilder.Models.Function
{
    public class FunctionModel : IFunctionModel
    {
        public string TypeIdentifier { get; }

        public string DisplayName { get; }

        public string DisplayColor { get; }

        public IInvokeModel Invoke { get; internal set; }

        public ICollection<IDispatchModel> Dispatchs { get; } = new List<IDispatchModel>();

        public ICollection<IInputModel> Inputs { get; } = new List<IInputModel>();

        public ICollection<IOutputModel> Outputs { get; } = new List<IOutputModel>();

        public FunctionModel(string typeIdentifier, string displayName = null, string displayColor = null)
        {
            TypeIdentifier = typeIdentifier;
            DisplayName = displayName;
            DisplayColor = displayColor;
        }

        [JsonConstructor()]
        public FunctionModel(string typeIdentifier, string displayName, string displayColor, CallerModel invoke, IEnumerable<CallerModel> dispatchs, IEnumerable<InputModel> inputs, IEnumerable<OutputModel> outputs) : this(typeIdentifier, displayName, displayColor)
        {
            Invoke = invoke;
            Dispatchs = dispatchs.Select(d => d as IDispatchModel).ToList();
            Inputs = inputs.Select(d => d as IInputModel).ToList();
            Outputs = outputs.Select(d => d as IOutputModel).ToList();
        }
    }
}