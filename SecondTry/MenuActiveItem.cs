using System;
using System.Web.Mvc;
namespace SecondTry
{
    public static class MenuActiveItem
    {
        public static string MakeActive(this UrlHelper urlhelper,string controller, string lastView)
        {
            string result = "active";

            string controllerName = lastView;

            if (!controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase)) {
                result = null;
            }

            return result;
        }
    }
}