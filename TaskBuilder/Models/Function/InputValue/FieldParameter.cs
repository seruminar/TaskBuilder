namespace TaskBuilder.Models.Function.InputValue
{
    /// <summary>
    /// Dynamic type that can be implicitly constructed from <see cref="string"/>, <see cref="bool"/>, <see cref="int"/>, or <see cref="float"/>.
    /// </summary>
    public sealed class FieldParameter
    {
        internal dynamic Value;

        private FieldParameter(dynamic stringParameter)
        {
            Value = stringParameter;
        }

        public static implicit operator string(FieldParameter parameter)
        {
            return parameter.Value;
        }

        public static implicit operator FieldParameter(string stringParameter)
        {
            return new FieldParameter(stringParameter);
        }

        public static implicit operator bool(FieldParameter parameter)
        {
            return parameter.Value;
        }

        public static implicit operator FieldParameter(bool boolParameter)
        {
            return new FieldParameter(boolParameter);
        }

        public static implicit operator int(FieldParameter parameter)
        {
            return parameter.Value;
        }

        public static implicit operator FieldParameter(int intParameter)
        {
            return new FieldParameter(intParameter);
        }

        public static implicit operator float(FieldParameter parameter)
        {
            return parameter.Value;
        }

        public static implicit operator FieldParameter(float floatParameter)
        {
            return new FieldParameter(floatParameter);
        }
    }
}