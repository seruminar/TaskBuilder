using System.Linq;

using CMS.DataEngine;

namespace TaskBuilder.Tasks
{
    /// <summary>
    /// Class providing <see cref="TaskSiteInfo"/> management.
    /// </summary>
    public partial class TaskSiteInfoProvider : AbstractInfoProvider<TaskSiteInfo, TaskSiteInfoProvider>
    {
        /// <summary>
        /// Returns all <see cref="TaskSiteInfo"/> bindings.
        /// </summary>
        public static ObjectQuery<TaskSiteInfo> GetTaskSites()
        {
            return ProviderObject.GetObjectQuery();
        }

        /// <summary>
        /// Returns <see cref="TaskSiteInfo"/> binding structure.
        /// </summary>
        /// <param name="taskId">ObjectType.taskbuilder_task ID.</param>
        /// <param name="siteId">Site ID.</param>
        public static TaskSiteInfo GetTaskSiteInfo(int taskId, int siteId)
        {
            return ProviderObject.GetObjectQuery().TopN(1)
                .WhereEquals("TaskID", taskId)
                .WhereEquals("SiteID", siteId)
                .FirstOrDefault();
        }

        /// <summary>
        /// Sets specified <see cref="TaskSiteInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="TaskSiteInfo"/> to set.</param>
        public static void SetTaskSiteInfo(TaskSiteInfo infoObj)
        {
            ProviderObject.SetInfo(infoObj);
        }

        /// <summary>
        /// Deletes specified <see cref="TaskSiteInfo"/> binding.
        /// </summary>
        /// <param name="infoObj"><see cref="TaskSiteInfo"/> object.</param>
        public static void DeleteTaskSiteInfo(TaskSiteInfo infoObj)
        {
            ProviderObject.DeleteInfo(infoObj);
        }

        /// <summary>
        /// Deletes <see cref="TaskSiteInfo"/> binding.
        /// </summary>
        /// <param name="taskId">ObjectType.taskbuilder_task ID.</param>
        /// <param name="siteId">Site ID.</param>
        public static void RemoveTaskFromSite(int taskId, int siteId)
        {
            var infoObj = GetTaskSiteInfo(taskId, siteId);
            if (infoObj != null)
            {
                DeleteTaskSiteInfo(infoObj);
            }
        }

        /// <summary>
        /// Creates <see cref="TaskSiteInfo"/> binding.
        /// </summary>
        /// <param name="taskId">ObjectType.taskbuilder_task ID.</param>
        /// <param name="siteId">Site ID.</param>
        public static void AddTaskToSite(int taskId, int siteId)
        {
            // Create new binding
            var infoObj = new TaskSiteInfo
            {
                TaskID = taskId,
                SiteID = siteId
            };

            // Save to the database
            SetTaskSiteInfo(infoObj);
        }
    }
}