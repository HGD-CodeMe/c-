using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        /// <summary>
        ///让所有控制器继承这个基本过滤器，如果用户没登录，则不能进行任何操作 
        ///让其重新登录
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if(Session["userInfo"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Login/Index");
                return;
            }
        }
    }
}