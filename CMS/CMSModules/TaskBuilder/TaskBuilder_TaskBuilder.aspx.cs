using System;
using CMS.Base.Web.UI;
using CMS.Core;
using CMS.Helpers;
using CMS.Membership;
using CMS.SiteProvider;
using CMS.UIControls;
using Newtonsoft.Json;
using TaskBuilder;
using TaskBuilder.Models.Diagram;
using TaskBuilder.Services;
using TaskBuilder.Tasks;

[Title("taskbuilder.ui.edittask")]
[UIElement("TaskBuilder", "TaskBuilder")]
[EditedObject(TaskInfo.OBJECT_TYPE, "objectid")]
public partial class TaskBuilder_TaskBuilder : CMSPage
{
    private readonly IFunctionModelService _functionModelService = Service.Resolve<IFunctionModelService>();

    /// <summary>
    /// Generated secure token in session.
    /// </summary>
    public string SecureToken => TaskBuilderHelper.GetSecureToken();

    protected void Page_Init()
    {
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/lodash.min.js", false);

        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/react.development.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/react-dom.development.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/main.js", false);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "Components",
            string.Join(string.Empty,
                TaskBuilderHelper.GetTransformedComponents("~/CMSScripts/CMSModules/TaskBuilder/Components/", RegexHelper.GetRegex("sandbox", true))), true);

        CssRegistration.RegisterCssLink(this, "~/CMSModules/TaskBuilder/Stylesheets/style.min.css");
        CssRegistration.RegisterCssLink(this, "~/CMSModules/TaskBuilder/Stylesheets/TaskBuilder.css");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var diagramAreaProps = new
        {
            allFunctions = _functionModelService.AllFunctionModels,
            authorizedFunctions = _functionModelService.GetAuthorizedFunctionModels(MembershipContext.AuthenticatedUser, SiteContext.CurrentSiteName),
            graph = EnsureTaskGraph(EditedObject as TaskInfo),
            graphMode = EnsureGraphMode(),
            secureToken = SecureToken
        };

        // Render graph area component
        var diagramComponent = TaskBuilderHelper.Environment.CreateComponent("TaskBuilder", diagramAreaProps, "task-builder", true);
        diagram.Text = diagramComponent.RenderHtml(true);

        // Initialize React event bindings and startup
        initScript.Text = ScriptHelper.GetScript(TaskBuilderHelper.Environment.GetInitJavaScript());

        if (!RequestHelper.IsAsyncPostback())
        {
            ScriptHelper.HideVerticalTabs(this);
        }
    }

    private TaskGraphModeEnum EnsureGraphMode()
    {
        if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource(TaskBuilderHelper.TASKBUILDER, "Modify"))
            return TaskGraphModeEnum.Readonly;

        return TaskGraphModeEnum.Full;
    }

    /// <summary>
    /// Get task diagram from database or start with empty one.
    /// </summary>
    /// <returns>Task graph JSON.</returns>
    private string EnsureTaskGraph(TaskInfo taskInfo)
    {
        return !string.IsNullOrEmpty(taskInfo.TaskGraph)
            ? taskInfo.TaskGraph
            : JsonConvert.SerializeObject(new DiagramModel(taskInfo.TaskGuid));
    }
}