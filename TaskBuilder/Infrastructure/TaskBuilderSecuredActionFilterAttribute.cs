using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using CMS.Helpers;

namespace TaskBuilder.Infrastructure
{
    internal class TaskBuilderSecuredActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            IEnumerable<string> values;

            if (actionContext.Request.Headers.TryGetValues("X-TB-Token", out values))
            {
                var secureToken = SessionHelper.GetValue(TaskBuilderHelper.TASKBUILDER_SECURE_TOKEN) as string;

                if (secureToken == values.FirstOrDefault())
                {
                    return;
                }
            }

            throw new HttpResponseException(HttpStatusCode.Forbidden);
        }
    }
}