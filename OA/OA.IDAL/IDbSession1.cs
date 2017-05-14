 

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
	
		IUserInfoDal UserInfoDal{get;set;}
	}	
}