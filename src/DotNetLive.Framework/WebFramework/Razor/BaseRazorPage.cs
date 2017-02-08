using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace DotNetLive.Framework.WebFramework.Razor
{
    public abstract class BaseRazorPage<T> : RazorPage<T>
    {
        //[RazorInjectAttribute]
        //public GlobalSettings GlobalSettings { get; private set; }
    }
}
