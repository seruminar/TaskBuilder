using CMS.DataEngine;

namespace TaskBuilder
{
    /// <summary>
    /// Class providing <see cref="ActionInfo"/> management.
    /// </summary>
    public partial class ActionInfoProvider : AbstractInfoProvider<ActionInfo, ActionInfoProvider>
    {
        /// <summary>
        /// Creates an instance of <see cref="ActionInfoProvider"/>.
        /// </summary>
        public ActionInfoProvider()
            : base(ActionInfo.TYPEINFO)
        {
        }

        /// <summary>
        /// Returns a query for all the <see cref="ActionInfo"/> objects.
        /// </summary>
        public static ObjectQuery<ActionInfo> GetActions()
        {
            return ProviderObject.GetObjectQuery();
        }

        /// <summary>
        /// Returns <see cref="ActionInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="ActionInfo"/> ID.</param>
        public static ActionInfo GetActionInfo(int id)
        {
            return ProviderObject.GetInfoById(id);
        }

        /// <summary>
        /// Returns <see cref="ActionInfo"/> with specified name.
        /// </summary>
        /// <param name="name"><see cref="ActionInfo"/> name.</param>
        public static ActionInfo GetActionInfo(string name)
        {
            return ProviderObject.GetInfoByCodeName(name);
        }

        /// <summary>
        /// Sets (updates or inserts) specified <see cref="ActionInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="ActionInfo"/> to be set.</param>
        public static void SetActionInfo(ActionInfo infoObj)
        {
            ProviderObject.SetInfo(infoObj);
        }

        /// <summary>
        /// Deletes specified <see cref="ActionInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="ActionInfo"/> to be deleted.</param>
        public static void DeleteActionInfo(ActionInfo infoObj)
        {
            ProviderObject.DeleteInfo(infoObj);
        }

        /// <summary>
        /// Deletes <see cref="ActionInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="ActionInfo"/> ID.</param>
        public static void DeleteActionInfo(int id)
        {
            ActionInfo infoObj = GetActionInfo(id);
            DeleteActionInfo(infoObj);
        }
    }
}