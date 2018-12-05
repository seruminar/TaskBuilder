using System;

using CMS.Base.Web.UI;
using CMS.UIControls;

using TaskBuilder;

[Title("taskbuilder.ui.edittask")]
[UIElement("TaskBuilder", "EditTask")]
public partial class TaskBuilder_Sandbox : MessagePage
{
    protected void Page_Init()
    {
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/lodash.min.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/react.development.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/react-dom.development.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/main.js", false);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "TaskBuilder", 
            DiagramFactory.Environment.Babel.TransformFile("~/CMSScripts/CMSModules/TaskBuilder/Components/TaskDiagramArea.jsx") +
            DiagramFactory.Environment.Babel.TransformFile("~/CMSScripts/CMSModules/TaskBuilder/Components/TaskDiagram.jsx") +
            DiagramFactory.Environment.Babel.TransformFile("~/CMSScripts/CMSModules/TaskBuilder/Components/BaseNodeFactory.jsx") +
            DiagramFactory.Environment.Babel.TransformFile("~/CMSScripts/CMSModules/TaskBuilder/Components/BaseNodeModel.jsx"), true);


        CssRegistration.RegisterCssLink(this, "~/CMSModules/TaskBuilder/Stylesheets/style.min.css");
        CssRegistration.RegisterCssLink(this, "~/CMSModules/TaskBuilder/Stylesheets/TaskBuilder.css");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var reactComponent = DiagramFactory.GetReactComponent("TaskDiagramArea");
        network.Text = reactComponent.RenderHtml(true);

        initScript.Text = ScriptHelper.GetScript(DiagramFactory.Environment.GetInitJavaScript());
    }
}