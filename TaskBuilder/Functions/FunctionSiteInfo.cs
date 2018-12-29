using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;

using TaskBuilder.Functions;

[assembly: RegisterObjectType(typeof(FunctionSiteInfo), FunctionSiteInfo.OBJECT_TYPE)]

namespace TaskBuilder.Functions
{
    /// <summary>
    /// Data container class for <see cref="FunctionSiteInfo"/>.
    /// </summary>
    [Serializable]
    public partial class FunctionSiteInfo : AbstractInfo<FunctionSiteInfo>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "taskbuilder.functionsite";

        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(FunctionSiteInfoProvider), OBJECT_TYPE, "TaskBuilder.FunctionSite", "FunctionSiteID", null, null, null, null, null, "SiteID", "FunctionID", "taskbuilder.function")
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
        /// Function site ID
        /// </summary>
		[DatabaseField]
        public virtual int FunctionSiteID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("FunctionSiteID"), 0);
            }
            set
            {
                SetValue("FunctionSiteID", value);
            }
        }

        /// <summary>
        /// Function ID
        /// </summary>
		[DatabaseField]
        public virtual int FunctionID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("FunctionID"), 0);
            }
            set
            {
                SetValue("FunctionID", value);
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
            FunctionSiteInfoProvider.DeleteFunctionSiteInfo(this);
        }

        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            FunctionSiteInfoProvider.SetFunctionSiteInfo(this);
        }

        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected FunctionSiteInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }

        /// <summary>
        /// Creates an empty instance of the <see cref="FunctionSiteInfo"/> class.
        /// </summary>
        public FunctionSiteInfo()
            : base(TYPEINFO)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FunctionSiteInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public FunctionSiteInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}