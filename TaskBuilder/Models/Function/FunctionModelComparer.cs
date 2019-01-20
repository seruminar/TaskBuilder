using System.Collections.Generic;

namespace TaskBuilder.Models.Function
{
    internal class FunctionModelComparer : EqualityComparer<FunctionModel>
    {
        public override bool Equals(FunctionModel x, FunctionModel y)
        {
            return string.Equals(x.TypeName, y.TypeName) &&
                    string.Equals(x.Assembly, y.Assembly);
        }

        public override int GetHashCode(FunctionModel obj)
        {
            return obj.TypeName.GetHashCode() ^ obj.Assembly.GetHashCode();
        }
    }
}