using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;

using TaskBuilder.Tasks;

[assembly: RegisterObjectType(typeof(TaskInfo), TaskInfo.OBJECT_TYPE)]

namespace TaskBuilder.Tasks
{
    /// <summary>
    /// Data container class for <see cref="TaskInfo"/>.
    /// </summary>
	[Serializable]
    public partial class TaskInfo : AbstractInfo<TaskInfo>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "taskbuilder.task";

        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(TaskInfoProvider), OBJECT_TYPE, "TaskBuilder.Task", "TaskID", "TaskLastModified", "TaskGuid", "TaskName", "TaskDisplayName", null, null, null, null)
        {
            ModuleName = "TaskBuilder",
            TouchCacheDependencies = true,
        };

        /// <summary>
        /// Task ID.
        /// </summary>
        [DatabaseField]
        public virtual int TaskID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("TaskID"), 0);
            }
            set
            {
                SetValue("TaskID", value);
            }
        }

        /// <summary>
        /// Task display name.
        /// </summary>
        [DatabaseField]
        public virtual string TaskDisplayName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("TaskDisplayName"), String.Empty);
            }
            set
            {
                SetValue("TaskDisplayName", value);
            }
        }

        /// <summary>
        /// Task name.
        /// </summary>
        [DatabaseField]
        public virtual string TaskName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("TaskName"), String.Empty);
            }
            set
            {
                SetValue("TaskName", value);
            }
        }

        /// <summary>
        /// Task graph.
        /// </summary>
        [DatabaseField]
        public virtual string TaskGraph
        {
            get
            {
                return ValidationHelper.GetString(GetValue("TaskGraph"), String.Empty);
            }
            set
            {
                SetValue("TaskGraph", value, String.Empty);
            }
        }

        /// <summary>
        /// Task guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid TaskGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("TaskGuid"), Guid.Empty);
            }
            set
            {
                SetValue("TaskGuid", value);
            }
        }

        /// <summary>
        /// Task last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime TaskLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("TaskLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("TaskLastModified", value);
            }
        }

        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            TaskInfoProvider.DeleteTaskInfo(this);
        }

        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            TaskInfoProvider.SetTaskInfo(this);
        }

        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected TaskInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }

        /// <summary>
        /// Creates an empty instance of the <see cref="TaskInfo"/> class.
        /// </summary>
        public TaskInfo()
            : base(TYPEINFO)
        {
        }

        /// <summary>
        /// Creates a new instances of the <see cref="TaskInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public TaskInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}