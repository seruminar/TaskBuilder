using System;
using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public class FunctionModel : IFunctionModel<CallerModel, CallerModel, InputModel, OutputModel>
    {
        public Guid TypeIdentifier { get; }

        public string DisplayName { get; }

        public string DisplayColor { get; }

        public CallerModel Invoke { get; internal set; }

        public ICollection<CallerModel> Dispatchs { get; } = new List<CallerModel>();

        public ICollection<InputModel> Inputs { get; } = new List<InputModel>();

        public ICollection<OutputModel> Outputs { get; } = new List<OutputModel>();

        public FunctionModel(Guid typeIdentifier, string displayName = null, string displayColor = null)
        {
            TypeIdentifier = typeIdentifier;
            DisplayName = displayName;
            DisplayColor = displayColor;
        }
    }
}