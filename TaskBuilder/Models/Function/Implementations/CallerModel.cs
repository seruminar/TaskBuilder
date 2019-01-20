namespace TaskBuilder.Models.Function
{
    public class CallerModel : IInvokeModel, IDispatchModel
    {
        public string Name { get; }

        public CallerModel(string name)
        {
            Name = name;
        }
    }
}