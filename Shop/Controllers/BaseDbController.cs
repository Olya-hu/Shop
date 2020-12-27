using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.DbConnection;

namespace Shop.Controllers
{
    public abstract class BaseDbController : Controller
    {
        protected readonly IShopConnection ShopConnection;

        protected BaseDbController(IShopConnection shopConnection)
        {
            ShopConnection = shopConnection;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ShopConnection.BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var exception = context.Exception;
            if (exception == null)
            {
                ShopConnection.CommitTransaction();
            }
            else
            {
                ShopConnection.RollbackTransaction();
                Response.StatusCode = 500;
            }
            base.OnActionExecuted(context);
        }
    }
}
