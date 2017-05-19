 

using OA.DAL;
using OA.IDAL;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.DALFactory
{
	public partial class DBSession : IDBSession
    {
	
		private IActionInfoDal _ActionInfoDal;
        public IActionInfoDal ActionInfoDal
        {
            get
            {
                if(_ActionInfoDal == null)
                {
                   // _ActionInfoDal = new ActionInfoDal();
				    _ActionInfoDal =AbstractFactory.CreateActionInfoDal();
                }
                return _ActionInfoDal;
            }
            set { _ActionInfoDal = value; }
        }
	
		private IDepartmentDal _DepartmentDal;
        public IDepartmentDal DepartmentDal
        {
            get
            {
                if(_DepartmentDal == null)
                {
                   // _DepartmentDal = new DepartmentDal();
				    _DepartmentDal =AbstractFactory.CreateDepartmentDal();
                }
                return _DepartmentDal;
            }
            set { _DepartmentDal = value; }
        }
	
		private IRoleInfoDal _RoleInfoDal;
        public IRoleInfoDal RoleInfoDal
        {
            get
            {
                if(_RoleInfoDal == null)
                {
                   // _RoleInfoDal = new RoleInfoDal();
				    _RoleInfoDal =AbstractFactory.CreateRoleInfoDal();
                }
                return _RoleInfoDal;
            }
            set { _RoleInfoDal = value; }
        }
	
		private Iuser_actionDal _user_actionDal;
        public Iuser_actionDal user_actionDal
        {
            get
            {
                if(_user_actionDal == null)
                {
                   // _user_actionDal = new user_actionDal();
				    _user_actionDal =AbstractFactory.Createuser_actionDal();
                }
                return _user_actionDal;
            }
            set { _user_actionDal = value; }
        }
	
		private IUserInfoDal _UserInfoDal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                if(_UserInfoDal == null)
                {
                   // _UserInfoDal = new UserInfoDal();
				    _UserInfoDal =AbstractFactory.CreateUserInfoDal();
                }
                return _UserInfoDal;
            }
            set { _UserInfoDal = value; }
        }
	
		private IWF_InstanceDal _WF_InstanceDal;
        public IWF_InstanceDal WF_InstanceDal
        {
            get
            {
                if(_WF_InstanceDal == null)
                {
                   // _WF_InstanceDal = new WF_InstanceDal();
				    _WF_InstanceDal =AbstractFactory.CreateWF_InstanceDal();
                }
                return _WF_InstanceDal;
            }
            set { _WF_InstanceDal = value; }
        }
	
		private IWF_StepInfoDal _WF_StepInfoDal;
        public IWF_StepInfoDal WF_StepInfoDal
        {
            get
            {
                if(_WF_StepInfoDal == null)
                {
                   // _WF_StepInfoDal = new WF_StepInfoDal();
				    _WF_StepInfoDal =AbstractFactory.CreateWF_StepInfoDal();
                }
                return _WF_StepInfoDal;
            }
            set { _WF_StepInfoDal = value; }
        }
	
		private IWF_TempDal _WF_TempDal;
        public IWF_TempDal WF_TempDal
        {
            get
            {
                if(_WF_TempDal == null)
                {
                   // _WF_TempDal = new WF_TempDal();
				    _WF_TempDal =AbstractFactory.CreateWF_TempDal();
                }
                return _WF_TempDal;
            }
            set { _WF_TempDal = value; }
        }
	}	
}