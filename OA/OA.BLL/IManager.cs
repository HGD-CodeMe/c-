 
using OA.IBLL;
using OA.Model;
using OA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
	
	public partial class ActionInfoService :BaseService<ActionInfo>,IActionInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.ActionInfoDal;
        }
    }   
	
	public partial class DepartmentService :BaseService<Department>,IDepartmentService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.DepartmentDal;
        }
    }   
	
	public partial class RoleInfoService :BaseService<RoleInfo>,IRoleInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.RoleInfoDal;
        }
    }   
	
	public partial class user_actionService :BaseService<user_action>,Iuser_actionService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.user_actionDal;
        }
    }   
	
	public partial class UserInfoService :BaseService<UserInfo>,IUserInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.UserInfoDal;
        }
    }   
	
	public partial class WF_InstanceService :BaseService<WF_Instance>,IWF_InstanceService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.WF_InstanceDal;
        }
    }   
	
	public partial class WF_StepInfoService :BaseService<WF_StepInfo>,IWF_StepInfoService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.WF_StepInfoDal;
        }
    }   
	
	public partial class WF_TempService :BaseService<WF_Temp>,IWF_TempService
    {
        public override void SetCurretnDal()
        {
            CurrentDal = this.GetCurrentDbSession.WF_TempDal;
        }
    }   
	
}