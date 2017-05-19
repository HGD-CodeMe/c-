 
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.IDAL
{
   
	
	public partial interface IActionInfoDal :IBaseDal<ActionInfo>
    {
      
    }
	
	public partial interface IDepartmentDal :IBaseDal<Department>
    {
      
    }
	
	public partial interface IRoleInfoDal :IBaseDal<RoleInfo>
    {
      
    }
	
	public partial interface Iuser_actionDal :IBaseDal<user_action>
    {
      
    }
	
	public partial interface IUserInfoDal :IBaseDal<UserInfo>
    {
      
    }
	
	public partial interface IWF_InstanceDal :IBaseDal<WF_Instance>
    {
      
    }
	
	public partial interface IWF_StepInfoDal :IBaseDal<WF_StepInfo>
    {
      
    }
	
	public partial interface IWF_TempDal :IBaseDal<WF_Temp>
    {
      
    }
	
}