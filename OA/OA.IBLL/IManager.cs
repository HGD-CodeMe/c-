 
using OA.Model;
using OA.Model.SearchParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.IBLL
{
	
	public partial interface IActionInfoService : IBaseService<ActionInfo>
    {
       
    }   
	
	public partial interface IDepartmentService : IBaseService<Department>
    {
       
    }   
	
	public partial interface IRoleInfoService : IBaseService<RoleInfo>
    {
       
    }   
	
	public partial interface Iuser_actionService : IBaseService<user_action>
    {
       
    }   
	
	public partial interface IUserInfoService : IBaseService<UserInfo>
    {
       
    }   
	
	public partial interface IWF_InstanceService : IBaseService<WF_Instance>
    {
       
    }   
	
	public partial interface IWF_StepInfoService : IBaseService<WF_StepInfo>
    {
       
    }   
	
	public partial interface IWF_TempService : IBaseService<WF_Temp>
    {
       
    }   
	
}