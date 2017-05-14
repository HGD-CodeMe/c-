using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OA.IDAL;
using OA.Model;

namespace OA.DAL
{
    //先继承，后实现
    public partial class UserInfoDal : BaseDal<UserInfo>,IUserInfoDal
    {
        //定义自己特有的方法
       
    }

    public partial class RoleInfoDal : BaseDal<RoleInfo>, IRoleInfoDal
    {
    }

    public partial class ActionInfoDal : BaseDal<ActionInfo>, IActionInfoDal
    {

    }
}
