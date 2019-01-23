using System.Collections.Generic;
using System.Linq;

namespace TaskBuilder.Models.Function.InputValue
{
    public class InputValueModel : IInputValueModel
    {
        public IDictionary<string, IFieldModel> Fields { get; }

        public InputValueModel(IDictionary<string, FieldModel> fields)
        {
            Fields = fields.ToDictionary(k => k.Key, v => v.Value as IFieldModel);
        }
    }
}