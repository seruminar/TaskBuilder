using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;

using TaskBuilder.Functions;

[assembly: RegisterObjectType(typeof(FunctionInfo), FunctionInfo.OBJECT_TYPE)]

namespace TaskBuilder.Functions
{
    /// <summary>
    /// Data container class for <see cref="FunctionInfo"/>.
    /// </summary>
	[Serializable]
    public partial class FunctionInfo : AbstractInfo<FunctionInfo>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "taskbuilder.function";

        private const string FUNCTION_NAME_PROXY = "FunctionName";

        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(FunctionInfoProvider), OBJECT_TYPE, "TaskBuilder.Function", "FunctionID", "FunctionLastModified", "FunctionGuid", null, FUNCTION_NAME_PROXY, null, null, null, null)
        {
            ModuleName = TaskBuilderHelper.TASKBUILDER,
            TouchCacheDependencies = true,
            OrderColumn = nameof(FunctionID)
        };

        /// <summary>
        /// Function ID.
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
        /// Function type guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid FunctionTypeGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("FunctionTypeGuid"), Guid.Empty);
            }
            set
            {
                SetValue("FunctionTypeGuid", value);
            }
        }

        /// <summary>
        /// Function guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid FunctionGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("FunctionGuid"), Guid.Empty);
            }
            set
            {
                SetValue("FunctionGuid", value);
            }
        }

        /// <summary>
        /// Function last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime FunctionLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("FunctionLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("FunctionLastModified", value);
            }
        }

        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            FunctionInfoProvider.DeleteFunctionInfo(this);
        }

        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            FunctionInfoProvider.SetFunctionInfo(this);
        }

        public override object GetValue(string columnName)
        {
            if (columnName == FUNCTION_NAME_PROXY)
            {
                var typeGuid = Guid.Parse(base.GetValue(nameof(FunctionTypeGuid)).ToString());

                return FunctionTypeInfoProvider.GetFunctionTypeInfo(typeGuid)?.FunctionTypeClass;
            }

            return base.GetValue(columnName);
        }

        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected FunctionInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }

        /// <summary>
        /// Creates an empty instance of the <see cref="FunctionInfo"/> class.
        /// </summary>
        public FunctionInfo()
            : base(TYPEINFO)
        {
        }

        /// <summary>
        /// Creates a new instances of the <see cref="FunctionInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public FunctionInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}