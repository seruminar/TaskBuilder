using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public class FunctionModel : IFunctionModel<CallerModel, CallerModel, InputModel, OutputModel>
    {
        public string TypeName { get; }

        public string Assembly { get; }

        public string DisplayName { get; }

        public string DisplayColor { get; }

        public CallerModel Invoke { get; internal set; }

        public ICollection<CallerModel> Dispatchs { get; } = new List<CallerModel>();

        public ICollection<InputModel> Inputs { get; } = new List<InputModel>();

        public ICollection<OutputModel> Outputs { get; } = new List<OutputModel>();

        public FunctionModel(string typeName, string assembly, string displayName = null, string displayColor = null)
        {
            TypeName = typeName;
            Assembly = assembly;
            DisplayName = displayName;
            DisplayColor = displayColor;
        }
    }
}