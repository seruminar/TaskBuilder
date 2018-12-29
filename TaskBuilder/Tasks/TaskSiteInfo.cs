using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using TaskBuilder.Tasks;

[assembly: RegisterObjectType(typeof(TaskSiteInfo), TaskSiteInfo.OBJECT_TYPE)]

namespace TaskBuilder.Tasks
{
    /// <summary>
    /// Data container class for <see cref="TaskSiteInfo"/>.
    /// </summary>
    [Serializable]
    public partial class TaskSiteInfo : AbstractInfo<TaskSiteInfo>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "taskbuilder.tasksite";

        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(TaskSiteInfoProvider), OBJECT_TYPE, "TaskBuilder.TaskSite", "TaskSiteID", null, null, null, null, null, "SiteID", "TaskID", "taskbuilder.task")
        {
            ModuleName = "TaskBuilder",
            TouchCacheDependencies = true,
            DependsOn = new List<ObjectDependency>()
            {
                new ObjectDependency("SiteID", PredefinedObjectType.SITE, ObjectDependencyEnum.Binding),
            },
            IsBinding = true
        };

        /// <summary>
        /// Task site ID
        /// </summary>
		[DatabaseField]
        public virtual int TaskSiteID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("TaskSiteID"), 0);
            }
            set
            {
                SetValue("TaskSiteID", value);
            }
        }

        /// <summary>
        /// Task ID
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
        /// Site ID
        /// </summary>
		[DatabaseField]
        public virtual int SiteID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("SiteID"), 0);
            }
            set
            {
                SetValue("SiteID", value);
            }
        }

        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            TaskSiteInfoProvider.DeleteTaskSiteInfo(this);
        }

        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            TaskSiteInfoProvider.SetTaskSiteInfo(this);
        }

        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected TaskSiteInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }

        /// <summary>
        /// Creates an empty instance of the <see cref="TaskSiteInfo"/> class.
        /// </summary>
        public TaskSiteInfo()
            : base(TYPEINFO)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TaskSiteInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public TaskSiteInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}