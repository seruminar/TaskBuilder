using System;
using System.Web;
using CMS;
using CMS.Base;
using CMS.Base.Web.UI;
using CMS.UIControls;

using TaskBuilder.UI;

[assembly: RegisterCustomClass(nameof(FunctionGridExtender), typeof(FunctionGridExtender))]

namespace TaskBuilder.UI
{
    public class FunctionGridExtender : ControlExtender<UniGrid>
    {
        public override void OnInit()
        {
            Control.OnExternalDataBound += OnExternalDataBound;
        }

        private object OnExternalDataBound(object sender, string sourceName, object parameter)
        {
            switch (sourceName.ToLowerCSafe())
            {
                case "gettypename":
                    var typeInfo = FunctionTypeInfoProvider.GetFunctionTypeInfo(Guid.Parse(parameter.ToString()));
                    return HttpUtility.HtmlEncode(typeInfo?.FunctionDisplayName);
            }

            return parameter;
        }
    }
}