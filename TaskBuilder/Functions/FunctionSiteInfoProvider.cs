using System.Linq;

using CMS.DataEngine;

namespace TaskBuilder.Functions
{
    /// <summary>
    /// Class providing <see cref="FunctionSiteInfo"/> management.
    /// </summary>
    public partial class FunctionSiteInfoProvider : AbstractInfoProvider<FunctionSiteInfo, FunctionSiteInfoProvider>
    {
        /// <summary>
        /// Returns all <see cref="FunctionSiteInfo"/> bindings.
        /// </summary>
        public static ObjectQuery<FunctionSiteInfo> GetFunctionSites()
        {
            return ProviderObject.GetObjectQuery();
        }

        /// <summary>
        /// Returns <see cref="FunctionSiteInfo"/> binding structure.
        /// </summary>
        /// <param name="functionId">ObjectType.taskbuilder_function ID.</param>
        /// <param name="siteId">Site ID.</param>
        public static FunctionSiteInfo GetFunctionSiteInfo(int functionId, int siteId)
        {
            return ProviderObject.GetObjectQuery().TopN(1)
                .WhereEquals("FunctionID", functionId)
                .WhereEquals("SiteID", siteId)
                .FirstOrDefault();
        }

        /// <summary>
        /// Sets specified <see cref="FunctionSiteInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="FunctionSiteInfo"/> to set.</param>
        public static void SetFunctionSiteInfo(FunctionSiteInfo infoObj)
        {
            ProviderObject.SetInfo(infoObj);
        }

        /// <summary>
        /// Deletes specified <see cref="FunctionSiteInfo"/> binding.
        /// </summary>
        /// <param name="infoObj"><see cref="FunctionSiteInfo"/> object.</param>
        public static void DeleteFunctionSiteInfo(FunctionSiteInfo infoObj)
        {
            ProviderObject.DeleteInfo(infoObj);
        }

        /// <summary>
        /// Deletes <see cref="FunctionSiteInfo"/> binding.
        /// </summary>
        /// <param name="functionId">ObjectType.taskbuilder_function ID.</param>
        /// <param name="siteId">Site ID.</param>
        public static void RemoveFunctionFromSite(int functionId, int siteId)
        {
            var infoObj = GetFunctionSiteInfo(functionId, siteId);
            if (infoObj != null)
            {
                DeleteFunctionSiteInfo(infoObj);
            }
        }

        /// <summary>
        /// Creates <see cref="FunctionSiteInfo"/> binding.
        /// </summary>
        /// <param name="functionId">ObjectType.taskbuilder_function ID.</param>
        /// <param name="siteId">Site ID.</param>
        public static void AddFunctionToSite(int functionId, int siteId)
        {
            // Create new binding
            var infoObj = new FunctionSiteInfo
            {
                FunctionID = functionId,
                SiteID = siteId
            };

            // Save to the database
            SetFunctionSiteInfo(infoObj);
        }
    }
}