using OA.BLL;
using OA.Model;
using OA.Model.Enum;
using OA.Model.SearchParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static OA.BLL.UserInfoService;

namespace OA.WebApp.Controllers
{
    public class UserInfoController : BaseController
    {
        // GET: UserInfo
        IBLL.IUserInfoService UserInfoService = new BLL.UserInfoService();
        IBLL.IRoleInfoService RoleInfoService = new RoleInfoService();
        IBLL.IActionInfoService ActionInfoService = new ActionInfoService();
        IBLL.Iuser_actionService user_actionService = new user_actionService();
        public ActionResult Index()
        {
            return View();
        }

        #region 获取用户数据
        public ActionResult GetUserInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            string name = Request["name"];
            string remark = Request["remark"];
            //构建搜索条件
            int totalCount = 0;
            UserInfoParam userInfoParam = new UserInfoParam()
            {

                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                UserName = name,
                Remark = remark
            };

            //short delFlag = (short)DelFlagEnum.Normarl;
            //var userInfoList=UserInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => c.DelFlag == delFlag, c => c.id, true);

            var userInfoList = UserInfoService.LoadSerachEntities(userInfoParam);
            var temp = from u in userInfoList
                       select new { ID = u.id, UserName = u.UName, UserPass = u.UPwd, Remark = u.Remark, RegTime = u.SubTime };
            return Json(new { rows = temp, total = userInfoParam.TotalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除用户数据
        public ActionResult DeleteUserInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(int.Parse(id));
            }
            if (UserInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }

        }
        #endregion

        #region 添加用户信息
        public ActionResult AddUserInfo(UserInfo userInfo)
        {
                    
            userInfo.DelFlag = 0;
            userInfo.ModeifiedOn = DateTime.Now;
            userInfo.SubTime = DateTime.Now;
            UserInfoService.AddEntity(userInfo);
            return Content("ok");
        }
        #endregion

        #region 查询要修改的数据
        public ActionResult GetUserInfoModel()
        {
            int id = int.Parse(Request["id"]);
            UserInfo userInfo = UserInfoService.LoadEntities(u => u.id == id).FirstOrDefault();
            if (userInfo != null)
            {
                //return Json(new { serverData = userInfo, msg = "ok" }, JsonRequestBehavior.AllowGet);
                return Content((Common.SerializerHelper.SerializeToString(new { serverData = userInfo, msg = "ok" })));
            }
            else
            {
                //这里会产生循环引用的问题
                //return Json(new { msg = "no" }, JsonRequestBehavior.AllowGet);
                return Content((Common.SerializerHelper.SerializeToString(new { msg = "no" })));
            }
        }
        #endregion

        #region 完成用户信息的修改
        public ActionResult EditUserInfo(UserInfo userInfo)
        {
            userInfo.ModeifiedOn = DateTime.Now;
            //这一句话不写 默认时间回报错；
            userInfo.SubTime = DateTime.Now;
            if (UserInfoService.EditEntity(userInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 为用户分配角色
        public ActionResult SetUserRoleInfo()
        {
            int userId = int.Parse(Request["userId"]);
            UserInfo userInfo = UserInfoService.LoadEntities(u => u.id == userId).FirstOrDefault();
            ViewBag.UserInfo = userInfo;
            //查询所有角色信息
            short  delFlag = (short)DelFlagEnum.Normarl;
            var roleInfolist = RoleInfoService.LoadEntities(r => r.DelFlag == delFlag).ToList();

            //找出用户已经有的编号
            var userRoleIdList = (from r in userInfo.RoleInfo
                                  select r.ID).ToList();

            ViewBag.AllRoleInfo = roleInfolist;
            ViewBag.AllExtRoleId = userRoleIdList;
            return View();
        }
        #endregion

        #region 完成用户角色的分配
        public ActionResult SetUserRole()
        {
            int userId = int.Parse(Request["userId"]);
            string[] AllKeys = Request.Form.AllKeys;
            List<int> list = new List<int>();
            foreach(string key in AllKeys)
            {
                if(key.StartsWith("cba_"))
                {
                    string k = key.Replace("cba_", "");
                    list.Add(int.Parse(k));
                }
            }
            if(UserInfoService.SetOrderInfo(userId,list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 对用户分配特殊权限
        public ActionResult SetUserActionInfo()
        {
            int userId = int.Parse(Request["userId"]);
            //查询要分配权限的用户信息
            var userInfo = UserInfoService.LoadEntities(u => u.id == userId).FirstOrDefault();
            ViewBag.userInfo = userInfo;
            //获取所有的权限信息
            short delFlag = (short)DelFlagEnum.Normarl;
            var AllActionList = ActionInfoService.LoadEntities(a => a.DelFlag == delFlag).ToList();
            //找出用户已经有的权限,这里是从两张表之间的关系表中去找
            var AllActionIdList = userInfo.user_action.ToList();

            ViewBag.ActionList = AllActionList;
            ViewBag.ActionIdList = AllActionIdList;

            return View();


        }
        #endregion

        #region 完成特殊权限分配
        public ActionResult SetActionUser()
        {
            int userId = int.Parse(Request["userId"]);
            int actionId = int.Parse(Request["actionId"]);

            bool isPass = Request["value"] == "true" ? true : false;

            if(UserInfoService.SetUserActionInfo(userId, actionId, isPass))
            {
                return Content("ok");
            }
            else
            {
               return Content("no");
            }
        }
        #endregion

        #region 清除用户权限
        public ActionResult ClearUserAction()
        {
            int userId = int.Parse(Request["userId"]);
            int actionId = int.Parse(Request["actionId"]);
            var UserInfo_ActionInfo = user_actionService.LoadEntities
                (r => r.User_ID == userId && r.Act_ID == actionId).FirstOrDefault();

            if (UserInfo_ActionInfo != null)
            {
                if (user_actionService.DeleteEntity(UserInfo_ActionInfo))
                {
                    return Content("ok");
                }
                else
                {
                    return Content("no");
                }
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
    }
}