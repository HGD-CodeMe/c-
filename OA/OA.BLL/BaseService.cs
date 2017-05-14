
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.BLL
{
    public abstract class BaseService<T>where T:class,new()
    {
        public IDAL.IDBSession GetCurrentDbSession
        {
            get
            {
                //这样导致每次请求都会new 一个对象
                // return new DbSession();//暂时
                return DALFactory.DBSessionFactory.CreateDbSession();
            }
        }

        //这个父类的属性用于获得子类的实例对象
        public IDAL.IBaseDal<T> CurrentDal { get; set; }
        public abstract void SetCurrentDal();
        public BaseService()
        {
            SetCurrentDal();
        }

        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            //这里是先通过子类知道具体的session值，
            //再调用方法获得值执行这个公共方法
            return CurrentDal.LoadEntities(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<s>(int pageIdex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda,
           System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            return CurrentDal.LoadPageEntities<s>(pageIdex, pageSize, out totalCount, whereLambda, orderbyLambda, isAsc);
        }

        public bool DeleteEntity(T entity)
        {
            CurrentDal.DeleteEntity(entity);
            return this.GetCurrentDbSession.SaveChanges();
        }

        public bool EditEntity(T entity)
        {
            CurrentDal.EditEntity(entity);
            return this.GetCurrentDbSession.SaveChanges();   
        }


        public T AddEntity(T entity)
        {
            CurrentDal.AddEntity(entity);
            this.GetCurrentDbSession.SaveChanges();
            return entity;
        }
    }

}
