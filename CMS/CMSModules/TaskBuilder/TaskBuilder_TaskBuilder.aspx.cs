using System;

using CMS.Base.Web.UI;
using CMS.Core;
using CMS.Helpers;
using CMS.UIControls;
using Newtonsoft.Json;
using TaskBuilder;
using TaskBuilder.Models;
using TaskBuilder.Services;

[Title("taskbuilder.ui.edittask")]
[UIElement("TaskBuilder", "TaskBuilder")]
[EditedObject(TaskInfo.OBJECT_TYPE, "objectid")]
public partial class TaskBuilder_TaskBuilder : CMSPage
{
    private readonly IFunctionModelService _functionModelService = Service.Resolve<IFunctionModelService>();

    public string SecureToken => TaskBuilderHelper.GetSecureToken();

    protected void Page_Init()
    {
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/lodash.min.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/react.development.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/react-dom.development.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/main.js", false);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "TaskBuilder",
            string.Join(string.Empty,
                TaskBuilderHelper.GetTransformedComponents("~/CMSScripts/CMSModules/TaskBuilder/Components/", RegexHelper.GetRegex("sandbox", true))), true);

        CssRegistration.RegisterCssLink(this, "~/CMSModules/TaskBuilder/Stylesheets/style.min.css");
        CssRegistration.RegisterCssLink(this, "~/CMSModules/TaskBuilder/Stylesheets/TaskBuilder.css");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get all available functions from IFunctionModelService
        var functions = _functionModelService.FunctionModels;

        // Get task diagram from database or start with empty one
        var task = EditedObject as TaskInfo;

        var graph = !string.IsNullOrEmpty(task.TaskGraph)
            ? task.TaskGraph
            : JsonConvert.SerializeObject(new DiagramModel(task.TaskGuid));

        // Generate secure token in session
        var secureToken = SecureToken;

        var diagramAreaProps = new
        {
            functions,
            graph,
            secureToken
        };

        // Render graph area component
        var diagramAreaComponent = TaskBuilderHelper.Environment.CreateComponent("TaskBuilder", diagramAreaProps, "task-builder", true);
        diagramArea.Text = diagramAreaComponent.RenderHtml(true);

        // Initialize React event bindings and startup
        initScript.Text = ScriptHelper.GetScript(TaskBuilderHelper.Environment.GetInitJavaScript());

        if (!RequestHelper.IsAsyncPostback())
        {
            ScriptHelper.HideVerticalTabs(this);
        }
    }
}