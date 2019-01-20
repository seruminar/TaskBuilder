using System.Collections.Generic;
using System.Linq;

using TaskBuilder.Models.Function;
using TaskBuilder.Services.Inputs;

namespace TaskBuilder.ValueBuilders
{
    public class StringValueOptionsBuilder : IValueOptionsModelBuilder<string>, IValueModelBuilder<string>
    {
        public string ConstructValue(InputFieldsModel inputValueModel)
        {
            return inputValueModel.Fields.FirstOrDefault(v => !string.IsNullOrEmpty(v.Value)).Value;
        }

        public InputFieldsModel NewFieldsModel()
        {
            ICollection<FieldModel> fields = new List<FieldModel>
            {
                new FieldModel("value", string.Empty)
            };

            return new InputFieldsModel(fields);
        }

        public InputFieldsModel FieldsModelFrom(params dynamic[] fieldsParams)
        {
            return new InputFieldsModel(fieldsParams.Select(p => new FieldModel((string)p, (FieldParameter)p)).ToList());
        }

        public InputFieldsModel OptionsModelFrom(params dynamic[] optionsParams)
        {
            return new InputFieldsModel(optionsParams.Select(p => new FieldModel((string)p, (FieldParameter)p)).ToList());
        }
    }
}