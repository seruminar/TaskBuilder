using System.Drawing;
using System.Text.RegularExpressions;

using CMS.Helpers;
using CMS.Localization;

namespace TaskBuilder.Services
{
    public class DisplayConverter : IDisplayConverter
    {
        private static readonly Color DEFAULT_DISPLAY_COLOR = Color.FromArgb(127, 255, 255, 255);

        public string GetDisplayColor(string typeName, Color? customColor = null)
        {
            if (!string.IsNullOrEmpty(typeName)
                && TaskBuilderHelper.DisplayColors.ContainsKey(typeName))
                return ColorTranslator.ToHtml(TaskBuilderHelper.DisplayColors[typeName]);

            return ColorTranslator.ToHtml(customColor ?? DEFAULT_DISPLAY_COLOR);
        }

        public string GetDescription(string description)
        {
            return ResHelper.GetString(description);
        }

        public string GetDisplayName(string displayName, string fullName, string name)
        {
            return !string.IsNullOrEmpty(displayName)
                ? ResHelper.GetString(displayName)
                : LocalizationHelper.GetString(fullName, null, defaultValue: null)
                  ?? Regex.Replace(name, "[a-z][A-Z]", m => $"{m.Value[0]} {m.Value[1]}");
        }
    }
}