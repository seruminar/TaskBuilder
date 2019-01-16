using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Hosting;

using CMS.Helpers;

using React;

namespace TaskBuilder
{
    public static class TaskBuilderHelper
    {
        public const string TASKBUILDER = nameof(TaskBuilder);

        public const string CACHE_REGISTER_KEY = "tbmodel";
        public static readonly int CACHE_MINUTES = 60;

        internal const string TASKBUILDER_SECURE_TOKEN = "TaskBuilderToken";
        private static readonly RandomNumberGenerator RNG = RandomNumberGenerator.Create();

        private const string CALLER = "caller";
        private const string PARAMETER = "parameter";
        private const string INVOKE = "invoke";
        private const string DISPATCH = "dispatch";
        private const string INPUT = "input";
        private const string OUTPUT = "output";

        public static ICollection<string> LinkTypes { get; } = new List<string>()
        {
            CALLER,
            PARAMETER
        };

        public static ICollection<string> PortTypes { get; } = new List<string>()
        {
            INVOKE,
            DISPATCH,
            INPUT,
            OUTPUT
        };

        public static Dictionary<string, Color> DisplayColors = new Dictionary<string, Color>() {
            { nameof(String), Color.SandyBrown }
        };

        /// <summary>
        /// Exposes ReactEnvironment.
        /// </summary>
        public static IReactEnvironment Environment => ReactEnvironment.GetCurrentOrThrow;

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