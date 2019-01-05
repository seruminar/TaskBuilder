namespace TaskBuilder.Models.Function
{
    public class MemberModel : TypedModel
    {
        public MemberModel(string name, string displayName, string displayType, string type) : base(name)
        {
            DisplayName = displayName;
            DisplayType = displayType;
            Type = type;
        }
    }
}