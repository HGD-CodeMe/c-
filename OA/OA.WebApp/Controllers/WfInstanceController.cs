using OA.BLL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workflow;

namespace OA.WebApp.Controllers
{
    public class WfInstanceController : BaseController
    {
        // GET: WfInstance
        IBLL.IWF_InstanceService WF_InstanceService = new WF_InstanceService();
        IBLL.IWF_TempService WF_TempService = new WF_TempService();
        IBLL.IUserInfoService UserInfoService = new UserInfoService();
        IBLL.IWF_StepInfoService WF_StepInfoService = new WF_StepInfoService();
        public ActionResult Index()
        {
            List<WF_Temp> list = WF_TempService.LoadEntities(w => w.DelFlag == 0).ToList();
            ViewData["list"] = list;
            return View();
        }

        #region 展示添加流程内容
        public ActionResult StartWorkflow()
        {
            int id = int.Parse(Request["id"]);
            ViewBag.Temp = WF_TempService.LoadEntities(w => w.ID == id).FirstOrDefault();
            var userInfoList = UserInfoService.LoadEntities(u => u.DelFlag == 0).ToList();

            var userInfoLists = from u in userInfoList
                                select new SelectListItem()
                                { Text = u.UName, Value = u.id.ToString(), Selected = false };

            ViewData["FlowTo"] = userInfoLists;
            return View();                   
        }
        #endregion

        #region 发起流程
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult StartWorkflow(WF_Instance wF_Instance)
        {
            //将流程列表中添加数据
            wF_Instance.ApplicationId = Convert.ToInt32(Guid.Empty);
            wF_Instance.Result = 0 + "";
            wF_Instance.StartedBy = loginUser.id.ToString();
            wF_Instance.Status = 0 + "";
            wF_Instance.SubTime = DateTime.Now;
            wF_Instance.WF_TempID = int.Parse(Request["hiddenTempId"]);
            WF_InstanceService.AddEntity(wF_Instance);
            //然后启动流程
            var dict = new Dictionary<string, object> { { "TempBookMarkName", "总监审批" } };

            Guid guid = Guid.Empty;
            WorkflowApplicationHelper.CreateWorkflow(new FincalActivity(),dict,out guid);

            wF_Instance.ApplicationId = Convert.ToInt32(guid);

            //保存步骤
            WF_StepInfo stepInfo = new WF_StepInfo();
            stepInfo.ChildStepID = 0;
            stepInfo.Comment = "开始进行财务审批";
            stepInfo.DelFlag = 0;
            stepInfo.IsProcessed = true;
            stepInfo.IsStartStep = true;
            stepInfo.IsEndStep = false;
            stepInfo.ProcessBy = loginUser.id;
            stepInfo.ParentStepID = 0;
            stepInfo.ParentStepID = 0;
            stepInfo.ProcessTime = DateTime.Now;
            stepInfo.Remark = "开始财务审批";
            stepInfo.StepName = "第一步";
            stepInfo.StepResult = 1+"";
            stepInfo.SubTime = DateTime.Now;
            stepInfo.Title = "开始财务审批";
            stepInfo.WF_InstanceID = wF_Instance.ID;

            WF_StepInfoService.AddEntity(stepInfo);

            //保存步骤
            WF_StepInfo masterStepInfo = new WF_StepInfo();
            masterStepInfo.ChildStepID = 0;
            masterStepInfo.Comment = "总监开始进行财务审批";
            masterStepInfo.DelFlag = 0;
            masterStepInfo.IsProcessed = false;
            masterStepInfo.IsStartStep = false;
            masterStepInfo.IsEndStep = false;
            masterStepInfo.ProcessBy = int.Parse(Request["FlowTo"]);
            masterStepInfo.ParentStepID = 0;
            masterStepInfo.ParentStepID = 0;
            masterStepInfo.ProcessTime = DateTime.Now;
            masterStepInfo.Remark = "总监开始财务审批";
            masterStepInfo.StepName = "总监审批";
            masterStepInfo.StepResult = 1 + "";
            masterStepInfo.SubTime = DateTime.Now;
            masterStepInfo.Title = "开始财务审批";
            masterStepInfo.WF_InstanceID = wF_Instance.ID;

            WF_StepInfoService.AddEntity(masterStepInfo);

            return Content("ok");
        }
        #endregion

    }
}