using System;

using CMS.Base.Web.UI;
using CMS.Helpers;
using CMS.UIControls;

using TaskBuilder;
using TaskBuilder.Models.Graph;
using TaskBuilder.Tasks;

[Title("taskbuilder.ui.tasksandbox")]
[UIElement("TaskBuilder", "TaskSandbox")]
public partial class TaskBuilder_TaskSandbox : CMSPage
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
        return TaskGraphMode.Sandbox;
    }

    /// <summary>
    /// Get task diagram from database or start with empty one.
    /// </summary>
    /// <returns>Task graph JSON.</returns>
    private Graph EnsureTaskGraph()
    {
        return new Graph(Guid.NewGuid());
    }
}