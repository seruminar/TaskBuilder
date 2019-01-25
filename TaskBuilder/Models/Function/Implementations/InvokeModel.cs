namespace TaskBuilder.Models.Function
{
    public class InvokeModel : IInvokeModel
    {
        public string Name { get; }

        public InvokeModel(string name)
        {
            Name = name;
        }
    }
}