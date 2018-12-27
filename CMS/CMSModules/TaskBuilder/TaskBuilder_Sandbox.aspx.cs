using System;

using CMS.Base.Web.UI;
using CMS.UIControls;

using TaskBuilder;

[Title("Task builder sandbox")]
[UIElement("TaskBuilder", "TaskSandbox")]
public partial class TaskBuilder_Sandbox : MessagePage
{
    protected void Page_Init()
    {
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/lodash.min.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/react.development.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/react-dom.development.js", false);
        ScriptHelper.RegisterScriptFile(this, "CMSModules/TaskBuilder/Vendor/main.js", false);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "TaskBuilder", TaskBuilderHelper.Environment.Babel.TransformFile("~/CMSScripts/CMSModules/TaskBuilder/Components/Sandbox.jsx"), true);

        CssRegistration.RegisterCssLink(this, "~/CMSModules/TaskBuilder/Stylesheets/style.min.css");
        CssRegistration.RegisterCssLink(this, "~/CMSModules/TaskBuilder/Stylesheets/TaskBuilder.css");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "Some text";
        var reactComponent = TaskBuilderHelper.Environment.CreateComponent("ErrorWrapper", new { }, clientOnly: true);
        network.Text = reactComponent.RenderHtml(true);

        initScript.Text = ScriptHelper.GetScript(TaskBuilderHelper.Environment.GetInitJavaScript());
    }
}