using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Hosting;

using CMS.DataEngine;
using CMS.Helpers;

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

        /// <summary>
        /// Exposes ReactEnvironment.
        /// </summary>
        public static IReactEnvironment Environment => ReactEnvironment.GetCurrentOrThrow;

        public static ICollection<string> LinkTypes { get; } = new List<string>()
        {
            "default",
            "caller",
            "property"
        };

        public static ICollection<string> PortTypes { get; } = new List<string>()
        {
            "enter",
            "leave",
            "input",
            "output"
        };

        /// <summary>
        /// Given a directory path, use Babel to transform all components in that path respecting the <paramref name="exclusion"/>.
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