using OA.BLL;
using OA.IBLL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
   
    public partial class ActionInfoService : BaseService<ActionInfo>,IActionInfoService
    {
        public bool SetActionRoleInfo(int actionId,List<int> roleIdList)
        {
            var actionInfo = this.GetCurrentDbSession.ActionInfoDal.LoadEntities(a => a.ID == actionId).FirstOrDefault();

            if(actionInfo != null)
            {
                actionInfo.RoleInfo.Clear();
                foreach(int roleId in roleIdList)
                {
                    var roleInfo = this.GetCurrentDbSession.RoleInfoDal.LoadEntities(r => r.ID == roleId).FirstOrDefault();
                    actionInfo.RoleInfo.Add(roleInfo);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
