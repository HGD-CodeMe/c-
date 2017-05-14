
using OA.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.Model.SearchParam;
using OA.Model.Enum;
using OA.Model;
using OA.IDAL;
using System.Linq.Expressions;
using System.IO;
using OA.DAL;

namespace OA.BLL
{
    public partial class UserInfoService : BaseService<UserInfo>,IUserInfoService
    {


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            var userInfoList = this.GetCurrentDbSession.UserInfoDal.LoadEntities(c => list.Contains(c.id));
            foreach(var userInfo in userInfoList)
            {
                this.GetCurrentDbSession.UserInfoDal.DeleteEntity(userInfo);
            }
            return this.GetCurrentDbSession.SaveChanges();
        }

        /// <summary>
        /// 多条件搜索用户信息
        /// </summary>
        /// <param name="userInfoSerachParam"></param>
        /// <returns></returns>
        public IQueryable<UserInfo> LoadSerachEntities(UserInfoParam userInfoSerachParam)
        {
            short delFlag = (short)DelFlagEnum.Normarl;
            var temp = this.GetCurrentDbSession.UserInfoDal.LoadEntities(c => c.DelFlag == delFlag);

            //拼接查询条件
            if(!string.IsNullOrEmpty(userInfoSerachParam.UserName))
            {
                temp = temp.Where<UserInfo>(u => u.UName.Contains(userInfoSerachParam.UserName));
            }
            if (!string.IsNullOrEmpty(userInfoSerachParam.Remark))
            {
                temp = temp.Where<UserInfo>(u => u.UName.Contains(userInfoSerachParam.Remark));
            }

            userInfoSerachParam.TotalCount = temp.Count();

            return temp.OrderBy<UserInfo, int>(u => u.id).Skip<UserInfo>
                ((userInfoSerachParam.PageIndex - 1) * userInfoSerachParam.PageSize).Take<UserInfo>
                (userInfoSerachParam.PageSize);


        }

        public bool SetOrderInfo(int userId, List<int> roleIdList)
        {
            var userInfo = this.GetCurrentDbSession.UserInfoDal.LoadEntities
                (u => u.id == userId).FirstOrDefault();

            if(userInfo != null)
            {
                //删除用户已有的角色,再重新赋值新的角色
                userInfo.RoleInfo.Clear();
                foreach(int roleId in roleIdList)
                {
                    var roleInfo = this.GetCurrentDbSession.RoleInfoDal.LoadEntities
                        (r => r.ID == roleId).FirstOrDefault();

                    userInfo.RoleInfo.Add(roleInfo);
                }
                return this.GetCurrentDbSession.SaveChanges();
            }
            else
            {
                return false;
            }
        }
    }
}
