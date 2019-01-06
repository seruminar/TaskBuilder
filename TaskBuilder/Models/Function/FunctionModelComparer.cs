using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    internal class FunctionModelComparer : EqualityComparer<IFunctionModel>
    {
        public override bool Equals(IFunctionModel x, IFunctionModel y)
        {
            return string.Equals(x.Type, y.Type);
        }

        public override int GetHashCode(IFunctionModel obj)
        {
            return obj.Type.GetHashCode();
        }
    }
}