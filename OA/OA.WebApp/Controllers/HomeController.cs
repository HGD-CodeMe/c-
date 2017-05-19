using OA.BLL;
using OA.Model;
using OA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        IBLL.IUserInfoService UserInfoService = new UserInfoService();
        UserInfo LoginUser = new UserInfo();
        public ActionResult Index()
        {
            return View();
        }

       public ActionResult HomePage()
        {
            return View();
        }

        #region 菜单权限过滤
        public ActionResult GetMenus()
        {
            //根据用户角色权限，将菜单权限取出来
            var loginUserInfo = UserInfoService.LoadEntities
                (u => u.id == LoginUser.id).FirstOrDefault();

            var loginUserRoleInfo = loginUserInfo.RoleInfo;//获取登录用户的角色信息

            //todo 这里可能有问题
            string actionTypeEnum = Convert.ToString(ActionInfoType.MenuActionTypeEnum);

            //查询角色所对应的菜单权限
            var loginUserActionInfos = (from r in loginUserRoleInfo
                                       from a in r.ActionInfo
                                       where a.ActionTypeEnum == actionTypeEnum
                                       select a).ToList();


            //根据用户---权限,根据用户查询中间表，然后利用导航属性查询权限表
            var login_r_user_action = from r in loginUserInfo.user_action
                                      select r.ActionInfo;

            var loginMenuAction = (from r in login_r_user_action
                                   where r.ActionTypeEnum == actionTypeEnum
                                   select r).ToList();

            //将两个集合合并
            loginUserActionInfos.AddRange(loginMenuAction);

            //查询登录用户所禁止的权限编号
            var loginForbActionInfo = (from r in loginUserInfo.user_action
                                      where r.isPass == false
                                      select r.Act_ID).ToList();

            //将禁止的权限过滤
            var loginUserAllowActionList = loginUserActionInfos.Where(a => !loginForbActionInfo.Contains(a.ID));
            //去除重复的权限
            var loginUserAllowActionLists = loginUserAllowActionList.Distinct(new EqualityComparer());

            var returnActionList = from a in loginUserAllowActionLists
                                   select new { icon = a.MenuIcon, title = a.ActionInfoName, url = a.Url };

            return Json(returnActionList, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}