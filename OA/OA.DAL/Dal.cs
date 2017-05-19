﻿ 

using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.DAL
{
		
	public partial class ActionInfoDal :BaseDal<ActionInfo>,IActionInfoDal
    {

    }
		
	public partial class DepartmentDal :BaseDal<Department>,IDepartmentDal
    {

    }
		
	public partial class RoleInfoDal :BaseDal<RoleInfo>,IRoleInfoDal
    {

    }
		
	public partial class user_actionDal :BaseDal<user_action>,Iuser_actionDal
    {

    }
		
	public partial class UserInfoDal :BaseDal<UserInfo>,IUserInfoDal
    {

    }
		
	public partial class WF_InstanceDal :BaseDal<WF_Instance>,IWF_InstanceDal
    {

    }
		
	public partial class WF_StepInfoDal :BaseDal<WF_StepInfo>,IWF_StepInfoDal
    {

    }
		
	public partial class WF_TempDal :BaseDal<WF_Temp>,IWF_TempDal
    {

    }
	
}