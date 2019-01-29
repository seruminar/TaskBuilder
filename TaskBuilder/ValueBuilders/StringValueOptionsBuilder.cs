using System.Collections.Generic;
using System.Linq;

using TaskBuilder.Functions;
using TaskBuilder.Models.Function.InputValue;
using TaskBuilder.Services.Inputs;

namespace TaskBuilder.ValueBuilders
{
    public class StringValueOptionsBuilder : IFilledValueBuilder<string>
    {
        public string BuildValue(IInputValueModel inputValueModel)
        {
            return inputValueModel.Fields.FirstOrDefault(v => !string.IsNullOrEmpty(v.Value[0])).Value[0];
        }

        public IInputValueModel GetStructureModel(params dynamic[] structureModelParams)
        {
            var fields = new Dictionary<string, FieldModel>
            {
                {"options", new FieldModel(FieldType.Dropdown) { Value = structureModelParams.Select(o => (FieldParameter)o).ToArray()} }
            };

            return new InputValueModel(fields);
        }

        public IInputValueModel GetFilledModel(IInputValueModel structureModel, params dynamic[] filledModelParams)
        {
            var fields = new Dictionary<string, FieldModel>
            {
                {"options", new FieldModel(FieldType.Dropdown) { Value = new FieldParameter[] { filledModelParams[0] } } }
            };

            return new InputValueModel(fields);
        }
    }
}