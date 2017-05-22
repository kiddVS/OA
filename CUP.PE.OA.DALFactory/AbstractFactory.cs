using CUP.PE.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.DALFactory
{
   public   partial class AbstractFactory
    {
       // private static readonly string _nameSpace = ConfigurationManager.AppSettings["NameSpace"];
      //  private static readonly string _assemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
        //public static IUserInfoDAL GetUserInfoInstance()
        //{
        //    string fullClassName = _nameSpace + ".UserInfoDAL";
        //    return GetObjectInstance(fullClassName) as IUserInfoDAL;
        //}
        private static object GetObjectInstance(string fullClassName)
        {
            Assembly dll = Assembly.Load(_assemblyPath);
           return dll.CreateInstance(fullClassName);
        }
    }
}
