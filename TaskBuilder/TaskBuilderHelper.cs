using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Hosting;

using CMS.DataEngine;
using CMS.Helpers;
using CMS.Localization;
using React;

namespace TaskBuilder
{
    public static class TaskBuilderHelper
    {
        internal static readonly string CACHE_INTERVAL_SETTINGS_KEY = "TBModelServiceCacheInterval";
        internal static readonly string CACHE_REGISTER_KEY = "tbmodel";

        internal static readonly int CACHE_MINUTES = SettingsKeyInfoProvider.GetIntValue(CACHE_INTERVAL_SETTINGS_KEY, CACHE_INTERVAL_SETTINGS_KEY, 60);

        public static readonly string TASKBUILDER = nameof(TaskBuilder);

        public static readonly string TASKBUILDER_SECURE_TOKEN = "TaskBuilderToken";
        private static readonly RandomNumberGenerator RNG = RandomNumberGenerator.Create();

        private static readonly Color DEFAULT_DISPLAY_COLOR = Color.FromArgb(127, 255, 255, 255);

        /// <summary>
        /// Exposes ReactEnvironment.
        /// </summary>
        public static IReactEnvironment Environment => ReactEnvironment.GetCurrentOrThrow;

        public static ICollection<string> LinkTypes { get; } = new List<string>()
        {
            "caller",
            "parameter"
        };

        public static ICollection<string> PortTypes { get; } = new List<string>()
        {
            "invoke",
            "dispatch",
            "input",
            "output"
        };

        internal static readonly Dictionary<string, Color?> DisplayColors = new Dictionary<string, Color?>() {
            { nameof(String), Color.SandyBrown }
        };

        internal static string GetDisplayName(string displayName, string fullName, string name)
        {
            return !string.IsNullOrEmpty(displayName)
                ? ResHelper.GetString(displayName)
                : LocalizationHelper.GetString(fullName, null, defaultValue: null)
                  ?? Regex.Replace(name, "[a-z][A-Z]", m => $"{m.Value[0]} {m.Value[1]}");
        }

        internal static string GetDisplayColor(string typeName, Color? customColor = null)
        {
            if (!string.IsNullOrEmpty(typeName))
            {
                Color? displayColor;

                DisplayColors.TryGetValue(typeName, out displayColor);

                return ColorTranslator.ToHtml(displayColor ?? DEFAULT_DISPLAY_COLOR);
            }

            return ColorTranslator.ToHtml(customColor ?? DEFAULT_DISPLAY_COLOR);
        }

        /// <summary>
        /// Given a directory path, use Babel to transform all components in that path respecting the
        /// <paramref name="exclusion"/>.
        /// </summary>
        public static IEnumerable<string> GetTransformedComponents(string componentsFolder, Regex exclusion)
        {
            var fullFolder = HostingEnvironment.MapPath(componentsFolder);

            foreach (var fullFilePath in Directory.EnumerateFiles(fullFolder, "*", SearchOption.AllDirectories))
            {
                if (!exclusion.IsMatch(fullFilePath))
                {
                    yield return Environment.Babel.TransformFileWithSourceMap(URLHelper.UnMapPath(fullFilePath), false).Code;
                }
            }
        }

        public static string GetSecureToken()
        {
            var token = SessionHelper.GetValue(TASKBUILDER_SECURE_TOKEN) as string;
            if (token == null)
            {
                var key = new byte[256];
                RNG.GetBytes(key);

                var stringKey = Convert.ToBase64String(key);

                SessionHelper.SetValue(TASKBUILDER_SECURE_TOKEN, stringKey);

                return stringKey;
            }

            return token;
        }
    }
}