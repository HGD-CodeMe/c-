using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Model.SearchParam
{
    public class UserInfoParam : BaseParam
    {
        //分装用户的搜索信息
        public string UserName { get; set; }
        public string Remark { get; set; }
       
    }
}
