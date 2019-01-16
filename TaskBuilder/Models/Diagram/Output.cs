using TaskBuilder.Models.Function;

namespace TaskBuilder.Models.Diagram
{
    public class Output : IOutputModel
    {
        public string Name { get; set; }

        public string TypeName { get; set; }

        public string DisplayName { get; set; }

        public string DisplayColor { get; set; }

        public string Description { get; set; }
    }
}