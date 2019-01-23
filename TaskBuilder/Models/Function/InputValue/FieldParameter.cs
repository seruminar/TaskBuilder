namespace TaskBuilder.Models.Function.InputValue
{
    public sealed class FieldParameter
    {
        internal dynamic Value { get; }

        private FieldParameter(string stringParameter)
        {
            Value = stringParameter;
        }

        public static implicit operator string(FieldParameter parameter)
        {
            return parameter.Value.ToString();
        }

        public static implicit operator FieldParameter(string stringParameter)
        {
            return new FieldParameter(stringParameter);
        }
    }
}