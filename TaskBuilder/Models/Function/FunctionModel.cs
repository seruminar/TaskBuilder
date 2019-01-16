using System;
using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    public class FunctionModel : IFunctionModel
    {
        public string Name { get; }

        public string DisplayName { get; }

        public string DisplayColor { get; }

        public IInvokeModel Invoke { get; internal set; }

        public IDispatchModel Dispatch { get; internal set; }

        public ICollection<IInputModel> Inputs { get; } = new List<IInputModel>();

        public ICollection<IOutputModel> Outputs { get; } = new List<IOutputModel>();

        public FunctionModel(string name, string displayName = null, string displayColor = null)
        {
            Name = name;
            DisplayName = displayName;
            DisplayColor = displayColor;
        }

        internal void SetInvoke(IInvokeModel invokeModel)
        {
            throw new NotImplementedException();
        }

        internal void SetDispatch(IDispatchModel dispatchModel)
        {
            throw new NotImplementedException();
        }
    }
}