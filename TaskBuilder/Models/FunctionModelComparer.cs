using System.Collections.Generic;
using TaskBuilder.Models;

namespace TaskBuilder.Services
{
    internal class FunctionModelComparer : EqualityComparer<IFunctionModel>
    {
        public override bool Equals(IFunctionModel x, IFunctionModel y)
        {
            return string.Equals(x.Name, y.Name);
        }

        public override int GetHashCode(IFunctionModel obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}