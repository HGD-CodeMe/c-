using OA.BLL;
using OA.DAL;
using OA.Model;
using OA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static OA.BLL.UserInfoService;

namespace OA.WebApp.Controllers
{
    public class RoleInfoController : Controller
    {
        // GET: RoleInfo
        IBLL.IRoleInfoService RoleInfoService = new RoleInfoService();
     
    
        public ActionResult Index()
        {
            return View();
        }

        #region
      
        public ActionResult GetRoleInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount;
            short delFlag = (short)DelFlagEnum.Normarl;
            var roleInfoList = RoleInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, r => r.DelFlag == delFlag , r => r.ID, true);

            var temp = from r in roleInfoList
                       select new { ID = r.ID, RoleName = r.RoleName, Sort = r.Sort, SubTime = r.SubTime, Remark = r.Remark };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);

        }

        #endregion
    }
}