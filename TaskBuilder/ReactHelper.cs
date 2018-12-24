using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using React;

namespace TaskBuilder
{
    public static class ReactHelper
    {
        /// <summary>
        /// Exposes ReactEnvironment.
        /// </summary>
        public static IReactEnvironment Environment => ReactEnvironment.GetCurrentOrThrow;

        /// <summary>
        /// Given a directory path, use Babel to transform all components in that path (direct children only) respecting the <paramref name="exclusion"/>.
        /// </summary>
        public static IEnumerable<string> GetTransformedComponents(string componentsFolder, Regex exclusion)
        {
            var fullFolder = HostingEnvironment.MapPath(componentsFolder);

            foreach (var fullFilePath in Directory.EnumerateFiles(fullFolder))
            {
                if (!exclusion.IsMatch(fullFilePath))
                {
                    yield return Environment.Babel.TransformFileWithSourceMap(componentsFolder + Path.GetFileName(fullFilePath), false).Code;
                }
            }
        }
    }
}