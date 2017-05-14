using OA.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OA.DALFactory
{
    /// <summary>
    /// 抽象工厂：与工厂的本质是一样的，解决对象创建问题
    /// 抽象工厂是通过反射的方式创建类的实例的
    /// </summary>
    public partial class AbstractFactory
    {
       


        /// <summary>
        /// 反射实例化后的对象
        /// </summary>
        /// <param name="fullClassName"></param>
        /// <returns></returns>
        public static object CreateInstance(string DalAssemblyPath, string fullClassName)
        {
            var assembly = Assembly.Load(DalAssemblyPath);
            return assembly.CreateInstance(fullClassName);
        }

    }
}
