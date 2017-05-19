using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp.Controllers
{
    public class BaseController : Controller
    {
        public UserInfo loginUser = new UserInfo();
        // GET: Base
        /// <summary>
        ///让所有控制器继承这个基本过滤器，如果用户没登录，则不能进行任何操作 
        ///让其重新登录
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            

            //设置一个系统管理员
            if(loginUser.UName == "hgd")
            {
                return;
            }
            ////完成权限过滤
            //string actionUrl = Request.Url.AbsolutePath.ToLower();//获得绝对路径
            //string actionHttpMethod = Request.HttpMethod;//请求方式
            //IBLL.IActionInfoService ActionInfoService = new BLL.ActionInfoService();
            //IBLL.IUserInfoService UserInfoService = new BLL.UserInfoService();

           
            //var actionInfo =  ActionInfoService.LoadEntities
            //    (a => a.Url == actionUrl && a.HttpMethod == actionHttpMethod).FirstOrDefault();

            //if(actionInfo == null)
            //{
            //    Response.Redirect("/Error.html");
            //    return;
            //}


            ////判断登录用户是否有访问权限
            //var loginuUserInfo = UserInfoService.LoadEntities(u => u.id == loginUser.id).FirstOrDefault();

            //if (Session["userInfo"] == null)
            //{
            //    filterContext.HttpContext.Response.Redirect("/Login/Index");
            //    var r_userInfo_ActionInfo = (from a in loginuUserInfo.user_action
            //                                where a.Act_ID == actionInfo.ID
            //                                select a).FirstOrDefault();
                
            //    if(r_userInfo_ActionInfo != null)
            //    {
            //        if(r_userInfo_ActionInfo.isPass == true)
            //        {
            //            return;
            //        }
            //        else
            //        {
            //            Response.Redirect("/Error.html");
            //            return;
            //        }
            //    }
            //    //按照用户--角色--权限进行过滤 
            //    var loginUserRoleInfo = loginUser.RoleInfo;
            //    var loginUserCountAction = (from r in loginUserRoleInfo
            //                             from a in r.ActionInfo
            //                             where a.ID == actionInfo.ID
            //                             select a).Count();

            //    if(loginUserCountAction < 1)
            //    {
            //        Response.Redirect("/Error.html");
            //        return;
            //    }
               
            //}
        }
    }
}