using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Hosting;

using CMS.Base.Web.UI;
using CMS.Core;
using CMS.Helpers;
using CMS.Membership;
using CMS.SiteProvider;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using React;

using TaskBuilder.Functions;
using TaskBuilder.Infrastructure;
using TaskBuilder.Models.Function;
using TaskBuilder.Models.Graph;
using TaskBuilder.Models.TaskBuilder;
using TaskBuilder.Services.Functions;
using TaskBuilder.Tasks;

namespace TaskBuilder
{
    public static class TaskBuilderHelper
    {
        public const string TASKBUILDER = nameof(TaskBuilder);

        private static readonly RandomNumberGenerator RNG = RandomNumberGenerator.Create();

        /// <summary>
        /// Exposes ReactEnvironment.
        /// </summary>
        internal static IReactEnvironment Environment => ReactEnvironment.GetCurrentOrThrow;

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
        /// Creates an instance of the specified React JavaScript component and renders the HTML for the component.
        /// </summary>
        /// <returns>HTML of React component.</returns>
        public static string RenderComponent<T>(string componentName, T componentModel, string containerId) where T : class
        {
            return Environment.CreateComponent(componentName, componentModel, containerId, true).RenderHtml(true);
        }

        /// <summary>
        /// Renders the JavaScript required to initialise all components client-side. This will attach event handlers to the server-rendered HTML.
        /// </summary>
        /// <returns>Script enclosed in /<script/> tags.</returns>
        public static string GetInitJavaScript()
        {
            return ScriptHelper.GetScript(Environment.GetInitJavaScript());
        }

        /// <summary>
        /// Returns a <see cref="TaskBuilderModel"/> containing all object data needed for the React app.
        /// </summary>
        /// <param name="taskGraph">Method to return task graph JSON.</param>
        /// <param name="taskGraphMode">Method to set the task graph mode.</param>
        /// <returns></returns>
        public static TaskBuilderModel GetTaskBuilderModel(Func<Graph> taskGraph, Func<TaskGraphMode> taskGraphMode)
        {
            var functionModelService = Service.Resolve<IFunctionModelService>();

            return new TaskBuilderModel
            {
                Models = new TaskModelsModel(
                    functionModelService.AllFunctionModels.Select(f => f as FunctionModel),
                    functionModelService.AuthorizedFunctionGuids(MembershipContext.AuthenticatedUser, SiteContext.CurrentSiteName),
                    FunctionHelper.PortTypes,
                    FunctionHelper.LinkTypes
                    ),
                Graph = new TaskGraphModel(
                   taskGraph(),
                   taskGraphMode()
                ),
                Endpoints = new Dictionary<string, string>
                {
                    { "save", URLHelper.ResolveUrl("~/TaskBuilder/Tasks/SaveTask") },
                    { "run", URLHelper.ResolveUrl("~/TaskBuilder/Tasks/RunTask") }
                },
                SecureToken = GetSecureToken()
            };
        }

        /// <summary>
        /// Given a directory path, use Babel to transform all components in that path respecting the
        /// <paramref name="exclusion"/>.
        /// </summary>
        internal static IEnumerable<string> GetTransformedComponents(string componentsFolder, Regex exclusion)
        {
            var fullFolder = HostingEnvironment.MapPath(componentsFolder);

            foreach (var fullFilePath in Directory.EnumerateFiles(fullFolder, "*", SearchOption.AllDirectories))
            {
                if (!exclusion.IsMatch(fullFilePath))
                {
                    yield return Environment.Babel.TransformFileWithSourceMap(URLHelper.UnMapPath(fullFilePath)).Code;
                }
            }
        }

        /// <summary>
        /// Generated secure token in session.
        /// </summary>
        internal static string GetSecureToken()
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