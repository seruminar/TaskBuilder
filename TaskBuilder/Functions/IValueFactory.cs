using System.Collections.Generic;
using TaskBuilder.Models.Function;

namespace TaskBuilder.Functions.InputValueFactories
{
    public interface IValueFactory
    {
        InputValueModel DefaultValue(params object[] valueParams);

        IEnumerable<IInputValueModel> ValueOptions(params object[] valueParams);

        object ConstructValue(string valueData);
    }
}