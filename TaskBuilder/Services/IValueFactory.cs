using System.Collections.Generic;

using TaskBuilder.Models.Function;

namespace TaskBuilder.Services
{
    public interface IValueFactory
    {
        IInputValueModel GetDefaultValue(params object[] valueParams);

        IEnumerable<IInputValueModel> GetValueOptions(params object[] valueParams);

        object ConstructValue(string valueData);
    }
}