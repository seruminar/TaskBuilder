using System.Drawing;

using CMS;

using TaskBuilder.Services.Inputs;

[assembly: RegisterImplementation(typeof(IDisplayConverter), typeof(DisplayConverter), Priority = CMS.Core.RegistrationPriority.Fallback)]

namespace TaskBuilder.Services.Inputs
{
    public interface IDisplayConverter
    {
        string DisplayNameFrom(string displayName, string fullName, string name);

        string DescriptionFrom(string description);

        string DisplayColorFrom(string typeName, Color? customColor = null);
    }
}