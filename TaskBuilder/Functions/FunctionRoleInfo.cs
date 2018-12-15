using System;
using System.Data;
using System.Runtime.Serialization;
using System.Collections.Generic;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using TaskBuilder;

[assembly: RegisterObjectType(typeof(FunctionRoleInfo), FunctionRoleInfo.OBJECT_TYPE)]

namespace TaskBuilder
{
    /// <summary>
    /// Data container class for <see cref="FunctionRoleInfo"/>.
    /// </summary>
    [Serializable]
    public partial class FunctionRoleInfo : AbstractInfo<FunctionRoleInfo>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "taskbuilder.functionrole";


        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(FunctionRoleInfoProvider), OBJECT_TYPE, "TaskBuilder.FunctionRole", "FunctionRoleID", null, null, null, null, null, null, "FunctionID", "taskbuilder.function")
        {
			ModuleName = "TaskBuilder",
			TouchCacheDependencies = true,
            DependsOn = new List<ObjectDependency>() 
			{
			    new ObjectDependency("RoleID", "cms.role", ObjectDependencyEnum.Binding), 
            },
            IsBinding = true
        };


        /// <summary>
        /// Function role ID
        /// </summary>
		[DatabaseField]
        public virtual int FunctionRoleID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("FunctionRoleID"), 0);
            }
            set
            {
                SetValue("FunctionRoleID", value);
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
        /// Role ID
        /// </summary>
		[DatabaseField]
        public virtual int RoleID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("RoleID"), 0);
            }
            set
            {
                SetValue("RoleID", value);
            }
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            FunctionRoleInfoProvider.DeleteFunctionRoleInfo(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            FunctionRoleInfoProvider.SetFunctionRoleInfo(this);
        }

		
		/// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected FunctionRoleInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="FunctionRoleInfo"/> class.
        /// </summary>
        public FunctionRoleInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instance of the <see cref="FunctionRoleInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public FunctionRoleInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}