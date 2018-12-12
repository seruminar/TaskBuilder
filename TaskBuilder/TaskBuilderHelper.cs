using CMS.DataEngine;

namespace TaskBuilder
{
    internal static class TaskBuilderHelper
    {
        internal static readonly string CACHE_INTERVAL_SETTINGS_KEY = "TBModelServiceCacheInterval";
        internal static readonly string CACHE_REGISTER_KEY = "tbmodel";

        internal static readonly int CACHE_MINUTES = SettingsKeyInfoProvider.GetIntValue(CACHE_INTERVAL_SETTINGS_KEY, CACHE_INTERVAL_SETTINGS_KEY, 60);
    }
}