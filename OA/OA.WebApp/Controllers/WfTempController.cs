using OA.BLL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OA.WebApp.Controllers
{
    public class WfTempController : BaseController
    {
        // GET: WfTemp
        IBLL.IWF_TempService WF_TempService = new WF_TempService();
        public ActionResult Index()
        {
            List<WF_Temp> list = WF_TempService.LoadEntities(w => w.DelFlag == 0).ToList();
            ViewData["list"] = list;
            return View();
        }

        public ActionResult createTemp()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult createTemp(WF_Temp wF_Temp)
        {
            wF_Temp.ID = 1;
            wF_Temp.DelFlag = 0;
            wF_Temp.ModfiedOn = DateTime.Now;
            wF_Temp.SubBy = loginUser.id.ToString();
            wF_Temp.SubTime = DateTime.Now;
            wF_Temp.TempStatue = 0;
            WF_TempService.AddEntity(wF_Temp);
            return RedirectToAction("Index");
        }

        
    }
}