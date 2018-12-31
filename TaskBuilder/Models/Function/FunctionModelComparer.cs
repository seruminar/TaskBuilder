using System.Collections.Generic;
using TaskBuilder.Models;

namespace TaskBuilder.Services
{
    internal class FunctionModelComparer : EqualityComparer<ITypedModel>
    {
        public override bool Equals(ITypedModel x, ITypedModel y)
        {
            return string.Equals(x.Name, y.Name);
        }

        public override int GetHashCode(ITypedModel obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}