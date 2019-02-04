using System;
using System.Web;

using CMS.Helpers;
using CMS.Membership;
using CMS.Routing.Web;

using TaskBuilder.Infrastructure;

[assembly: RegisterHttpHandler("CMSScripts/CMSModules/TaskBuilder/GetComponents", typeof(GetTaskBuilderComponentsHandler))]

namespace TaskBuilder.Infrastructure
{
    internal class GetTaskBuilderComponentsHandler : AdvancedGetFileHandler
    {
        private CMSOutputResource componentsFile;

        public override bool AllowCache { get; set; } = true;

        protected override void ProcessRequestInternal(HttpContextBase context)
        {
            // Check the site
            if (string.IsNullOrEmpty(CurrentSiteName))
            {
                throw new Exception($"{nameof(GetTaskBuilderComponentsHandler)}: Site is not running.");
            }

            string cacheKey = $"{nameof(GetTaskBuilderComponentsHandler).ToLower()}|{CurrentSiteName}|{MembershipContext.AuthenticatedUser.UserName}";

            // Try to get data from cache
            using (var cs = new CachedSection<CMSOutputResource>(ref componentsFile, CacheMinutes, true, cacheKey))
            {
                if (cs.LoadData)
                {
                    // Process the file
                    SetComponentsFile();

                    cs.Data = componentsFile;
                }
            }

            if (componentsFile != null)
            {
                // Send the data
                SendFile(componentsFile);
            }
        }

        private void SetComponentsFile()
        {
            var components = string.Join(string.Empty,
                TaskBuilderHelper.GetTransformedComponents("~/CMSScripts/CMSModules/TaskBuilder/Components/", RegexHelper.GetRegex("sandbox", true)));

            // Create the output file
            componentsFile = new CMSOutputResource
            {
                Name = RequestContext.URL.ToString(),
                Data = components,
                Etag = $"{nameof(GetTaskBuilderComponentsHandler)}|{SecurityHelper.GetMD5Hash(components)}"
            };
        }

        /// <summary>
        /// Sends the given file within response.
        /// </summary>
        /// <param name="file">File to send</param>
        private void SendFile(CMSOutputResource file)
        {
            // Clear response.
            CookieHelper.ClearResponseCookies();
            Response.Clear();

            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

            // Send the file
            if ((file != null) && (file.Data != null))
            {
                // Prepare response
                Response.ContentType = "application/javascript; charset=utf-8";

                // Client caching - only on the live site
                if (AllowCache && CacheHelper.CacheImageEnabled(CurrentSiteName) && ETagsMatch(file.Etag, file.LastModified))
                {
                    RespondNotModified(file.Etag);
                    return;
                }

                if (AllowCache && CacheHelper.CacheImageEnabled(CurrentSiteName))
                {
                    SetTimeStamps(file.LastModified);

                    Response.Cache.SetETag(file.Etag);
                }

                // Add the file data
                Response.Write(file.Data);
            }

            CompleteRequest();
        }
    }
}