using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.IDAL
{
    /// <summary>
    /// 封装的是公共方法
    /// T后面是一个相当于约束的东西，必须传一个能够被new的class类
    /// </summary>
    public interface IBaseDal<T>where T:class,new()
    {
        //方法中的参数，基本查询，延迟加载
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);
        //s 是方法的泛型，当我们调用这个方法的时候，传入这个泛型，同时这是个分页排序的查询方法
        IQueryable<T> LoadPageEntities<s>(int pageIdex, int pageSize, out int totalCount,
            System.Linq.Expressions.Expression<Func<T, bool>> whereLambda,
            System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc);
        //删除
        bool DeleteEntity(T entity);
        //更新
        bool EditEntity(T entity);
        //添加
        T AddEntity(T entity);
    }
}
