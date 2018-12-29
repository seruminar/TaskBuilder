using CMS.Base;
using CMS.DataEngine;
using CMS.Membership;
using CMS.SiteProvider;

namespace TaskBuilder.Functions
{
    /// <summary>
    /// Class providing <see cref="FunctionInfo"/> management.
    /// </summary>
    public partial class FunctionInfoProvider : AbstractInfoProvider<FunctionInfo, FunctionInfoProvider>
    {
        /// <summary>
        /// Creates an instance of <see cref="FunctionInfoProvider"/>.
        /// </summary>
        public FunctionInfoProvider()
            : base(FunctionInfo.TYPEINFO)
        {
        }

        /// <summary>
        /// Returns a query for all the <see cref="FunctionInfo"/> objects.
        /// </summary>
        public static ObjectQuery<FunctionInfo> GetFunctions()
        {
            return ProviderObject.GetObjectQuery();
        }

        /// <summary>
        /// Returns a query for all the <see cref="FunctionInfo"/> objects available for given <see cref="UserInfo"/>.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static ObjectQuery<FunctionInfo> GetFunctionsForUserAndSite(IUserInfo user, SiteInfoIdentifier site)
        {
            return ProviderObject.GetFunctionsForUserAndSiteInternal(user, site);
        }

        /// <summary>
        /// Returns <see cref="FunctionInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="FunctionInfo"/> ID.</param>
        public static FunctionInfo GetFunctionInfo(int id)
        {
            return ProviderObject.GetInfoById(id);
        }

        /// <summary>
        /// Returns <see cref="FunctionInfo"/> with specified name.
        /// </summary>
        /// <param name="name"><see cref="FunctionInfo"/> name.</param>
        public static FunctionInfo GetFunctionInfo(string name)
        {
            return ProviderObject.GetInfoByCodeName(name);
        }

        /// <summary>
        /// Sets (updates or inserts) specified <see cref="FunctionInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="FunctionInfo"/> to be set.</param>
        public static void SetFunctionInfo(FunctionInfo infoObj)
        {
            ProviderObject.SetInfo(infoObj);
        }

        /// <summary>
        /// Deletes specified <see cref="FunctionInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="FunctionInfo"/> to be deleted.</param>
        public static void DeleteFunctionInfo(FunctionInfo infoObj)
        {
            ProviderObject.DeleteInfo(infoObj);
        }

        /// <summary>
        /// Deletes <see cref="FunctionInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="FunctionInfo"/> ID.</param>
        public static void DeleteFunctionInfo(int id)
        {
            FunctionInfo infoObj = GetFunctionInfo(id);
            DeleteFunctionInfo(infoObj);
        }

        protected virtual ObjectQuery<FunctionInfo> GetFunctionsForUserAndSiteInternal(IUserInfo user, SiteInfoIdentifier site)
        {
            var query = GetFunctions();

            site = site ?? SiteContext.CurrentSiteName;

            if (user.CheckPrivilegeLevel(UserPrivilegeLevelEnum.Admin, site))
            {
                return query;
            }

            return query
                    .WhereIn("FunctionID",
                        FunctionRoleInfoProvider.GetFunctionRoles()
                        .WhereIn("RoleID",
                            UserRoleInfoProvider
                            .GetUserRoles()
                            .Column("RoleID")
                            .WhereEquals("UserID", user.UserID)
                        )
                        .Column("FunctionID")
                    )
                    .And()
                    .WhereIn("FunctionID",
                        FunctionSiteInfoProvider.GetFunctionSites()
                        .WhereEquals("SiteID",
                            SiteInfoProvider
                             .GetSiteID(site)
                        )
                        .Column("FunctionID")
                    );
        }
    }
}