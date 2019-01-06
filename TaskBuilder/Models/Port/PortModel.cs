using System.Drawing;

using TaskBuilder.Models.Function;

namespace TaskBuilder.Models.Diagram
{
    public class PortModel : ColoredModel
    {
        public PortModel(string name, string displayName, string displayType, PortTypeEnum type, Color? displayColor = null)
        {
            Type = type.ToString().ToLower();
            DisplayName = displayName ?? name;
            DisplayType = displayType;

            _displayColors.TryGetValue(displayType, out displayColor);

            DisplayColor = ColorTranslator.ToHtml(displayColor ?? DEFAULT_DISPLAY_COLOR);
        }
    }
}