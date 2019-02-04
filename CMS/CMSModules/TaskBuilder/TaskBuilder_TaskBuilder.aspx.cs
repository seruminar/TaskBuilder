using System;

using CMS.Base.Web.UI;
using CMS.Helpers;
using CMS.Membership;
using CMS.UIControls;

using TaskBuilder;
using TaskBuilder.Models.Diagram;
using TaskBuilder.Tasks;

[Title("taskbuilder.ui.edittask")]
[UIElement("TaskBuilder", "TaskBuilder")]
[EditedObject(TaskInfo.OBJECT_TYPE, "objectid")]
public partial class TaskBuilder_TaskBuilder : CMSPage
{
    protected void Page_Init()
    {
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/lodash.min.js", false);

        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/react.development.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/react-dom.development.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/main.js", false);

        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/GetComponents", false);

        CssRegistration.RegisterCssLink(this, "~/CMSModules/TaskBuilder/Stylesheets/style.min.css");
        CssRegistration.RegisterCssLink(this, "~/CMSModules/TaskBuilder/Stylesheets/TaskBuilder.css");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Render graph area component
        diagram.Text = TaskBuilderHelper.RenderTaskBuilder(TaskBuilderHelper.GetTaskBuilderModel(EnsureTaskGraph, EnsureGraphMode), "task-builder");

        // Initialize React event bindings and startup
        initScript.Text = TaskBuilderHelper.GetInitJavaScript();

        if (!RequestHelper.IsAsyncPostback())
        {
            ScriptHelper.HideVerticalTabs(this);
        }
    }

    private TaskGraphMode EnsureGraphMode()
    {
        if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource(TaskBuilderHelper.TASKBUILDER, "Modify"))
            return TaskGraphMode.Readonly;

        return TaskGraphMode.Full;
    }

    /// <summary>
    /// Get task diagram from database or start with empty one.
    /// </summary>
    private string EnsureTaskGraph()
    {
        var editedTask = EditedObject as TaskInfo;

        return !string.IsNullOrEmpty(editedTask.TaskGraph)
            ? editedTask.TaskGraph
            : new Diagram(editedTask.TaskGuid).ToJson();
    }
}