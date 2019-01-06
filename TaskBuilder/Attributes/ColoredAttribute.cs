using System.Drawing;

namespace TaskBuilder.Attributes
{
    public class ColoredAttribute : NamedAttribute
    {
        public Color? DisplayColor { get; }

        public ColoredAttribute(string displayName = null, Color? displayColor = null) : base(displayName)
        {
            DisplayColor = displayColor;
        }
    }
}