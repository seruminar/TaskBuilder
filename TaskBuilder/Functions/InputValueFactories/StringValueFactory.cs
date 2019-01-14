using System.Collections.Generic;
using System.Linq;
using TaskBuilder.Models.Function;

namespace TaskBuilder.Functions.InputValueFactories
{
    public class StringValueFactory : IValueFactory
    {
        public InputValueModel DefaultValue(params object[] valueParams)
        {
            return new InputValueModel(valueParams[0].ToString(), valueParams);
        }

        public IEnumerable<IInputValueModel> ValueOptions(params object[] valueParams)
        {
            return valueParams.Select(p => new InputValueModel(p.ToString(), new[] { p }));
        }

        public object ConstructValue(string valueData)
        {
            return valueData;
        }
    }
}