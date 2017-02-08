using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetLive.Framework.WebFramework.Filters
{
    public class PermissionValidateAttribute : ActionFilterAttribute, IActionFilter
    {
        public string PermissionName { get; set; }
        public PermissionValidateAttribute(string permissionName)
        {
            this.PermissionName = permissionName;
        }
    }
}
