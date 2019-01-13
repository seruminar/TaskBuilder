namespace TaskBuilder.Models.Function
{
    internal class CallerModel : IInvokeModel, IDispatchModel
    {
        public string Name { get; }

        public CallerModel(string name)
        {
            Name = name.ToLower();
        }
    }
}