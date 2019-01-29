using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Hosting;

using CMS.Helpers;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using React;

using TaskBuilder.Functions;
using TaskBuilder.Infrastructure;

namespace TaskBuilder
{
    public static class TaskBuilderHelper
    {
        public const string TASKBUILDER = nameof(TaskBuilder);

        private static readonly RandomNumberGenerator RNG = RandomNumberGenerator.Create();

        /// <summary>
        /// Exposes ReactEnvironment.
        /// </summary>
        public static IReactEnvironment Environment => ReactEnvironment.GetCurrentOrThrow;

        public static JsonSerializerSettings JsonSerializerSettings
        {
            get
            {
                var settings = new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                settings.Converters.Add(new FieldParameterConverter());
                settings.Converters.Add(new StringEnumConverter(true));

                return settings;
            }
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
            var token = SessionHelper.GetValue(TaskBuilderSecuredActionFilterAttribute.TASKBUILDER_SECURE_TOKEN) as string;
            if (token == null)
            {
                var key = new byte[256];
                RNG.GetBytes(key);

                var stringKey = Convert.ToBase64String(key);

                SessionHelper.SetValue(TaskBuilderSecuredActionFilterAttribute.TASKBUILDER_SECURE_TOKEN, stringKey);

                return stringKey;
            }

            return token;
        }
    }
}