using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;

using TaskBuilder;

[assembly: RegisterObjectType(typeof(ActionInfo), ActionInfo.OBJECT_TYPE)]

namespace TaskBuilder
{
    /// <summary>
    /// Data container class for <see cref="ActionInfo"/>.
    /// </summary>
	[Serializable]
    public partial class ActionInfo : AbstractInfo<ActionInfo>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "taskbuilder.action";

        /// <summary>
        /// Type information.
        /// </summary>
#warning "You will need to configure the type info."

        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(ActionInfoProvider), OBJECT_TYPE, "TaskBuilder.Action", "ActionID", "ActionLastModified", "ActionGuid", "ActionName", "ActionDisplayName", null, null, null, null)
        {
            ModuleName = "TaskBuilder",
            TouchCacheDependencies = true,
        };

        /// <summary>
        /// Action ID.
        /// </summary>
        [DatabaseField]
        public virtual int ActionID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("ActionID"), 0);
            }
            set
            {
                SetValue("ActionID", value);
            }
        }

        /// <summary>
        /// Action name.
        /// </summary>
        [DatabaseField]
        public virtual string ActionName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ActionName"), String.Empty);
            }
            set
            {
                SetValue("ActionName", value);
            }
        }

        /// <summary>
        /// Action display name.
        /// </summary>
        [DatabaseField]
        public virtual string ActionDisplayName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ActionDisplayName"), String.Empty);
            }
            set
            {
                SetValue("ActionDisplayName", value);
            }
        }

        /// <summary>
        /// Action behavior assembly.
        /// </summary>
        [DatabaseField]
        public virtual string ActionBehaviorAssembly
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ActionBehaviorAssembly"), String.Empty);
            }
            set
            {
                SetValue("ActionBehaviorAssembly", value);
            }
        }

        /// <summary>
        /// Action behavior class.
        /// </summary>
        [DatabaseField]
        public virtual string ActionBehaviorClass
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ActionBehaviorClass"), String.Empty);
            }
            set
            {
                SetValue("ActionBehaviorClass", value, String.Empty);
            }
        }

        /// <summary>
        /// Action node model code.
        /// </summary>
        [DatabaseField]
        public virtual string ActionNodeModelCode
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ActionNodeModelCode"), String.Empty);
            }
            set
            {
                SetValue("ActionNodeModelCode", value);
            }
        }

        /// <summary>
        /// Action node factory code.
        /// </summary>
        [DatabaseField]
        public virtual string ActionNodeFactoryCode
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ActionNodeFactoryCode"), String.Empty);
            }
            set
            {
                SetValue("ActionNodeFactoryCode", value);
            }
        }

        /// <summary>
        /// Action node widget code.
        /// </summary>
        [DatabaseField]
        public virtual string ActionNodeWidgetCode
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ActionNodeWidgetCode"), String.Empty);
            }
            set
            {
                SetValue("ActionNodeWidgetCode", value);
            }
        }

        /// <summary>
        /// Action port model code.
        /// </summary>
        [DatabaseField]
        public virtual string ActionPortModelCode
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ActionPortModelCode"), String.Empty);
            }
            set
            {
                SetValue("ActionPortModelCode", value);
            }
        }

        /// <summary>
        /// Action port factory code.
        /// </summary>
        [DatabaseField]
        public virtual string ActionPortFactoryCode
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ActionPortFactoryCode"), String.Empty);
            }
            set
            {
                SetValue("ActionPortFactoryCode", value);
            }
        }

        /// <summary>
        /// Action guid.
        /// </summary>
        [DatabaseField]
        public virtual Guid ActionGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("ActionGuid"), Guid.Empty);
            }
            set
            {
                SetValue("ActionGuid", value);
            }
        }

        /// <summary>
        /// Action last modified.
        /// </summary>
        [DatabaseField]
        public virtual DateTime ActionLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("ActionLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("ActionLastModified", value);
            }
        }

        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            ActionInfoProvider.DeleteActionInfo(this);
        }

        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            ActionInfoProvider.SetActionInfo(this);
        }

        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected ActionInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }

        /// <summary>
        /// Creates an empty instance of the <see cref="ActionInfo"/> class.
        /// </summary>
        public ActionInfo()
            : base(TYPEINFO)
        {
        }

        /// <summary>
        /// Creates a new instances of the <see cref="ActionInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public ActionInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}