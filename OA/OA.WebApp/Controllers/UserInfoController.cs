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
        public ActionResult Index()
        {
            return View();
        }

        #region 获取用户数据
        public ActionResult GetActionInfoList()
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

            short delFlag = (short)DelFlagEnum.Normarl;
            var userInfoList=UserInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => c.DelFlag == delFlag, c => c.id, true);

          //  var userInfoList = UserInfoService.LoadSerachEntities(userInfoParam);
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


    }
}