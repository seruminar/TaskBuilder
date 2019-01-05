using System.Collections.Generic;

namespace TaskBuilder.Models.Function
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