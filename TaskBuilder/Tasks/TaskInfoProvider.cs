using System;
using CMS.DataEngine;
using Newtonsoft.Json;
using TaskBuilder.Models;

namespace TaskBuilder.Tasks
{
    /// <summary>
    /// Class providing <see cref="TaskInfo"/> management.
    /// </summary>
    public partial class TaskInfoProvider : AbstractInfoProvider<TaskInfo, TaskInfoProvider>
    {
        /// <summary>
        /// Creates an instance of <see cref="TaskInfoProvider"/>.
        /// </summary>
        public TaskInfoProvider()
            : base(TaskInfo.TYPEINFO)
        {
        }

        /// <summary>
        /// Returns a query for all the <see cref="TaskInfo"/> objects.
        /// </summary>
        public static ObjectQuery<TaskInfo> GetTasks()
        {
            return ProviderObject.GetObjectQuery();
        }

        /// <summary>
        /// Returns <see cref="TaskInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="TaskInfo"/> ID.</param>
        public static TaskInfo GetTaskInfo(int id)
        {
            return ProviderObject.GetInfoById(id);
        }

        /// <summary>
        /// Returns <see cref="TaskInfo"/> with specified GUID.
        /// </summary>
        /// <param name="guid"><see cref="TaskInfo"/> GUID.</param>
        public static TaskInfo GetTaskInfo(Guid guid)
        {
            return ProviderObject.GetInfoByGuid(guid);
        }

        /// <summary>
        /// Returns <see cref="TaskInfo"/> with specified name.
        /// </summary>
        /// <param name="name"><see cref="TaskInfo"/> name.</param>
        public static TaskInfo GetTaskInfo(string name)
        {
            return ProviderObject.GetInfoByCodeName(name);
        }

        /// <summary>
        /// Sets (updates or inserts) specified <see cref="TaskInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="TaskInfo"/> to be set.</param>
        public static void SetTaskInfo(TaskInfo infoObj)
        {
            ProviderObject.SetInfo(infoObj);
        }

        /// <summary>
        /// Sets (updates or inserts) a <see cref="TaskInfo"/> based on the provided <see cref="DiagramModel"/>.
        /// </summary>
        /// <param name="diagram"><see cref="DiagramModel"/> to be set.</param>
        public static void SetTaskInfo(DiagramModel diagram)
        {
            TaskInfo infoObj = GetTaskInfo(diagram.Id) ?? new TaskInfo();

            infoObj.TaskGraph = JsonConvert.SerializeObject(diagram);

            SetTaskInfo(infoObj);
        }

        /// <summary>
        /// Deletes specified <see cref="TaskInfo"/>.
        /// </summary>
        /// <param name="infoObj"><see cref="TaskInfo"/> to be deleted.</param>
        public static void DeleteTaskInfo(TaskInfo infoObj)
        {
            ProviderObject.DeleteInfo(infoObj);
        }

        /// <summary>
        /// Deletes <see cref="TaskInfo"/> with specified ID.
        /// </summary>
        /// <param name="id"><see cref="TaskInfo"/> ID.</param>
        public static void DeleteTaskInfo(int id)
        {
            TaskInfo infoObj = GetTaskInfo(id);
            DeleteTaskInfo(infoObj);
        }
    }
}