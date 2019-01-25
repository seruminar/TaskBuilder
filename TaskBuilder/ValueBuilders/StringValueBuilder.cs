using System.Collections.Generic;
using System.Linq;

using TaskBuilder.Models.Function.InputValue;
using TaskBuilder.Services.Inputs;

namespace TaskBuilder.ValueBuilders
{
    public class StringValueBuilder : IFilledValueBuilder<string>
    {
        public string BuildValue(IInputValueModel inputValueModel)
        {
            return inputValueModel.Fields.FirstOrDefault(v => !string.IsNullOrEmpty(v.Value[0])).Value[0];
        }

        public IInputValueModel GetStructureModel(params dynamic[] structureModelParams)
        {
            var fields = structureModelParams.ToDictionary(
                p => (string)p,
                _ => new FieldModel(FieldType.Text)
            );

            return new InputValueModel(fields);
        }

        public IInputValueModel GetFilledModel(IInputValueModel structureModel, params dynamic[] filledModelParams)
        {
            var filledFields = new Dictionary<string, FieldModel>(structureModel.Fields.Count);

            int i = 0;
            foreach (var field in structureModel.Fields)
            {
                filledFields.Add(field.Key, new FieldModel(FieldType.Text, filledModelParams[i]));
                i++;
            }

            return new InputValueModel(filledFields);
        }
    }
}