﻿ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.IDAL
{
	public partial interface IDBSession
    {

	
		IActionInfoDal ActionInfoDal{get;set;}
	
		IDepartmentDal DepartmentDal{get;set;}
	
		IRoleInfoDal RoleInfoDal{get;set;}
	
		Iuser_actionDal user_actionDal{get;set;}
	
		IUserInfoDal UserInfoDal{get;set;}
	
		IWF_InstanceDal WF_InstanceDal{get;set;}
	
		IWF_StepInfoDal WF_StepInfoDal{get;set;}
	
		IWF_TempDal WF_TempDal{get;set;}
	}	
}