using System.Drawing;

using CMS;

using TaskBuilder.Services;

[assembly: RegisterImplementation(typeof(IDisplayConverter), typeof(DisplayConverter), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services
{
    public interface IDisplayConverter
    {
        string GetDisplayName(string displayName, string fullName, string name);

        string GetDescription(string description);

        string GetDisplayColor(string typeName, Color? customColor = null);
    }
}