using System;

using CMS.Base.Web.UI;
using CMS.Helpers;
using CMS.Membership;
using CMS.UIControls;

using Newtonsoft.Json;

using TaskBuilder;
using TaskBuilder.Models.Graph;
using TaskBuilder.Tasks;

[Title("taskbuilder.ui.edittask")]
[UIElement("TaskBuilder", "TaskBuilder")]
[EditedObject(TaskInfo.OBJECT_TYPE, "objectid")]
public partial class TaskBuilder_TaskBuilder : CMSPage
{
    protected void Page_Init()
    {
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/taskBuilderDev.js", false);

        CssRegistration.RegisterCssLink(this, "~/CMSScripts/CMSModules/TaskBuilder/taskBuilderDev.css");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Render graph area component
        diagram.Text = TaskBuilderHelper.RenderComponent(TaskBuilderHelper.TASKBUILDER, TaskBuilderHelper.GetTaskBuilderModel(EnsureTaskGraph, EnsureGraphMode), "task-builder");

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
    private Graph EnsureTaskGraph()
    {
        var editedTask = EditedObject as TaskInfo;

        return !string.IsNullOrEmpty(editedTask.TaskGraph)
            ? JsonConvert.DeserializeObject<Graph>(editedTask.TaskGraph, TaskBuilderHelper.JsonSerializerSettings)
            : new Graph(editedTask.TaskGuid);
    }
}