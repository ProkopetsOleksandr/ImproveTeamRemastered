using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ImproveTeam.Extensions
{
    public static class HtmlExtensions
    {
        public static string ActiveClass(this IHtmlHelper htmlHelper, string controllers = null, string actions = null, string cssClass = "active")
        {
            var currentController = htmlHelper?.ViewContext.RouteData.Values["controller"] as string;
            var currentAction = htmlHelper?.ViewContext.RouteData.Values["action"] as string;

            var acceptedControllers = (controllers ?? currentController ?? string.Empty).Split(',');
            var acceptedActions = (actions ?? currentAction ?? string.Empty).Split(',');

            if (acceptedControllers.Contains(currentController) &&
                acceptedActions.Contains(currentAction))
            {
                return cssClass;
            }

            return string.Empty;
        }
    }
}
