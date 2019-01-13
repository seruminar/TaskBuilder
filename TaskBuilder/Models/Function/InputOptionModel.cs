namespace TaskBuilder.Models.Function
{
    public sealed class InputOptionModel
    {
        public string Value { get; }

        public string DisplayName { get; }

        public InputOptionModel(object obj)
        {
            var option = obj.ToString();

            if (!string.IsNullOrEmpty(option))
            {
                Value = option;
                DisplayName = option;
            }
        }
    }
}