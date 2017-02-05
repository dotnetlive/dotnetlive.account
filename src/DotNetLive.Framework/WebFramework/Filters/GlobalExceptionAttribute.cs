using DotNetLive.Framework.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetLive.Framework.WebFramework.Filters
{

    public class GlobalExceptionAttribute : ExceptionFilterAttribute, IExceptionFilter
    {
        public override void OnException(ExceptionContext context)
        {
            DbTransactionHelper.RollbackTransaction(context.HttpContext.RequestServices);

            //context.Result = new JsonResult(context.Exception);
            context.ExceptionHandled = false;
        }
    }
}
