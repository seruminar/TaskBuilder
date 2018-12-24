using System;

using CMS.Base.Web.UI;
using CMS.Core;
using CMS.Helpers;
using CMS.UIControls;

using TaskBuilder;
using TaskBuilder.Services;

[Title("taskbuilder.ui.edittask")]
[UIElement("TaskBuilder", "TaskBuilder")]
[EditedObject(TaskInfo.OBJECT_TYPE, "objectid")]
public partial class TaskBuilder_TaskBuilder : CMSPage
{
    private readonly IFunctionModelService _functionModelService = Service.Resolve<IFunctionModelService>();

    protected void Page_Init()
    {
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/lodash.min.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/react.development.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/react-dom.development.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/main.js", false);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "TaskBuilder",
            string.Join(string.Empty,
                ReactHelper.GetTransformedComponents("~/CMSScripts/CMSModules/TaskBuilder/Components/", RegexHelper.GetRegex("sandbox", true))), true);

        CssRegistration.RegisterCssLink(this, "~/CMSModules/TaskBuilder/Stylesheets/style.min.css");
        CssRegistration.RegisterCssLink(this, "~/CMSModules/TaskBuilder/Stylesheets/TaskBuilder.css");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get props from IFunctionModelService for all available functions
        var functionModels = _functionModelService.FunctionModels;

        // Get task diagram from database
        var task = EditedObject as TaskInfo;
        var taskGraph = task.TaskGraph;

        // Pass both as props
        var diagramAreaProps = new { functions = functionModels, graph = taskGraph };

        // Render graph area component
        var diagramAreaComponent = ReactHelper.Environment.CreateComponent("TaskDiagramArea", diagramAreaProps, "task-builder", true);
        diagramArea.Text = diagramAreaComponent.RenderHtml(true);

        // Initialize React event bindings and startup
        initScript.Text = ScriptHelper.GetScript(ReactHelper.Environment.GetInitJavaScript());

        if (!RequestHelper.IsAsyncPostback())
        {
            ScriptHelper.HideVerticalTabs(this);
        }
    }
}