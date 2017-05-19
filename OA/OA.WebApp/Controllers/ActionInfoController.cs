using OA.BLL;
using OA.Model;
using OA.Model.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static OA.BLL.UserInfoService;

namespace OA.WebApp.Controllers
{
    public class ActionInfoController : BaseController
    {
        // GET: ActionInfo
        IBLL.IActionInfoService ActionInfoService = new ActionInfoService();
        IBLL.IRoleInfoService RoleInfoService = new RoleInfoService();
        public ActionResult Index()
        {
            return View();
        }

        #region 获取用户权限信息
        public ActionResult GetActionInfoList()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount;
            short delFlag = (short)DelFlagEnum.Normarl;
            var actionInfoList = ActionInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, r => r.DelFlag == delFlag, r => r.ID, true);

            var temp = from a in actionInfoList
                       select new
                       {
                           ID = a.ID,
                           ActionInfoName = a.ActionInfoName,
                           Sort = a.Sort,
                           Remark = a.Remark,
                           Url = a.Url,
                           HttpMethod = a.HttpMethod,
                           ActionTypeEnum = a.ActionTypeEnum,
                           SubTime = a.SubTime
                       };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取上传图片
        public ActionResult FileUpload()
        {
            HttpPostedFileBase file = Request.Files["fileIconUp"];
            if(file != null)
            {
                string fileName = Path.GetFileName(file.FileName);//获取上传的文件名
                string fileExt = Path.GetExtension(fileName);//获取扩展名
                if(fileExt == ".jpg")
                {
                    string dir = "/MenuIcon/" + DateTime.Now.Year + "/" + DateTime.Now.Month
                        + "/" + DateTime.Now.Day + "/";

                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));

                    string filenewName = Guid.NewGuid().ToString();
                    string fullDir = dir + filenewName + fileExt;
                    file.SaveAs(Request.MapPath(fullDir));
                    return Content("yes:" + fullDir);
                }   
                else
                {
                    return Content("no:文件类型错误！！");
                } 
            }
            else
            {
                return Content("no:请选择上传的图片文件");
            }
        }
        #endregion

        #region 完成权限添加功能
        public ActionResult AddActionInfo(ActionInfo actionInfo)
        {
            actionInfo.DelFlag = 0 ;
            actionInfo.ModifiedOn = DateTime.Now;
            actionInfo.SubTime = DateTime.Now;
            actionInfo.Url = actionInfo.Url.ToLower();
            ActionInfoService.AddEntity(actionInfo);

            return Content("ok");
        }
        #endregion

        #region 为权限分配角色
        public ActionResult  SetActionRole()
        {
            int id = int.Parse(Request["id"]);
            var actioninfo = ActionInfoService.LoadEntities(a => a.ID == id).FirstOrDefault();
            ViewBag.ActionInfo = actioninfo;
            //获取所有的角色
            short delFlag = (short)DelFlagEnum.Normarl;
            var AllRoleList = RoleInfoService.LoadEntities(a => a.DelFlag == delFlag).ToList();
            //获取当前的权限已经有的权限信息
            var AllExtRoleIdList = (from r in actioninfo.RoleInfo
                                    select r.ID).ToList();

            ViewBag.RoleList = AllRoleList;
            ViewBag.RoleIdList = AllExtRoleIdList;

            return View();

        }
        #endregion

        #region
        public ActionResult SetActionRoleInfo()
        {
            int actionId = int.Parse(Request["actionId"]);
            List<int> list = new List<int>();
            //获取表单中所有的name属性的key值
            string[] Allkeys = Request.Form.AllKeys;

            foreach(string key in Allkeys)
            {
                if(key.StartsWith("cba_"))
                {
                    string k = key.Replace("cba_", "");
                    list.Add(int.Parse(k));
                }
            }

            if(ActionInfoService.SetActionRoleInfo(actionId,list))
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