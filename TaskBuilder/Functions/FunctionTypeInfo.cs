using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using TaskBuilder;

[assembly: RegisterObjectType(typeof(FunctionTypeInfo), FunctionTypeInfo.OBJECT_TYPE)]

namespace TaskBuilder
{
    /// <summary>
    /// Data container class for <see cref="FunctionTypeInfo"/>.
    /// </summary>
	[Serializable]
    public partial class FunctionTypeInfo : AbstractInfo<FunctionTypeInfo>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "taskbuilder.functiontype";

        /// <summary>
        /// Type information.
        /// </summary>
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(FunctionTypeInfoProvider), OBJECT_TYPE, "TaskBuilder.FunctionType", "FunctionTypeID", null, "FunctionTypeGuid", null, null, null, null, null, null)
        {
            ModuleName = TaskBuilderHelper.TASKBUILDER,
            TouchCacheDependencies = true,
        };

        /// <summary>
        /// Function type ID.
        /// </summary>
        [DatabaseField]
        public virtual int FunctionTypeID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("FunctionTypeID"), 0);
            }
            set
            {
                SetValue("FunctionTypeID", value);
            }
        }

        /// <summary>
        /// Function type class.
        /// </summary>
        [DatabaseField]
        public virtual string FunctionTypeClass
        {
            get
            {
                return ValidationHelper.GetString(GetValue("FunctionTypeClass"), String.Empty);
            }
            set
            {
                SetValue("FunctionTypeClass", value);
            }
        }

        /// <summary>
        /// Function type assembly.
        /// </summary>
        [DatabaseField]
        public virtual string FunctionTypeAssembly
        {
            get
            {
                return ValidationHelper.GetString(GetValue("FunctionTypeAssembly"), String.Empty);
            }
            set
            {
                SetValue("FunctionTypeAssembly", value);
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
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            FunctionTypeInfoProvider.DeleteFunctionTypeInfo(this);
        }

        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            FunctionTypeInfoProvider.SetFunctionTypeInfo(this);
        }

        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected FunctionTypeInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }

        /// <summary>
        /// Creates an empty instance of the <see cref="FunctionTypeInfo"/> class.
        /// </summary>
        public FunctionTypeInfo()
            : base(TYPEINFO)
        {
        }

        /// <summary>
        /// Creates a new instances of the <see cref="FunctionTypeInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public FunctionTypeInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }

        internal FunctionTypeInfo(string classFullName, string assemblyName)
            : base(TYPEINFO)
        {
            FunctionTypeClass = classFullName;
            FunctionTypeAssembly = assemblyName;
        }
    }
}