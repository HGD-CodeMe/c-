
using OA.Model;
using OA.Model.SearchParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.IBLL
{
    public partial interface IUserInfoService : IBaseService<UserInfo>
    {
        bool DeleteEntities(List<int> list);
        IQueryable<UserInfo> LoadSerachEntities(UserInfoParam userInfoSerachParam);

        bool SetOrderInfo(int userId, List<int> list);
    }

}
