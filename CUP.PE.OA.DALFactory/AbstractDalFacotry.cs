 

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
    public partial  class AbstractFactory
    {
      private static readonly string _nameSpace = ConfigurationManager.AppSettings["NameSpace"];
        private static readonly string _assemblyPath = ConfigurationManager.AppSettings["AssemblyPath"];
   
		
	    public static IActionInfoDAL GetActionInfoInstance()
        {

            string fullClassName = _nameSpace + ".ActionInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            return GetObjectInstance(fullClassName) as IActionInfoDAL;
        }
		
	    public static IBooksDAL GetBooksInstance()
        {

            string fullClassName = _nameSpace + ".BooksDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            return GetObjectInstance(fullClassName) as IBooksDAL;
        }
		
	    public static IDepartmentDAL GetDepartmentInstance()
        {

            string fullClassName = _nameSpace + ".DepartmentDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            return GetObjectInstance(fullClassName) as IDepartmentDAL;
        }
		
	    public static IR_UserInfo_ActionInfoDAL GetR_UserInfo_ActionInfoInstance()
        {

            string fullClassName = _nameSpace + ".R_UserInfo_ActionInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            return GetObjectInstance(fullClassName) as IR_UserInfo_ActionInfoDAL;
        }
		
	    public static IRoleInfoDAL GetRoleInfoInstance()
        {

            string fullClassName = _nameSpace + ".RoleInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            return GetObjectInstance(fullClassName) as IRoleInfoDAL;
        }
		
	    public static IUserInfoDAL GetUserInfoInstance()
        {

            string fullClassName = _nameSpace + ".UserInfoDAL";


            //object obj = Assembly.Load(ConfigurationManager.AppSettings["DalAssembly"]).CreateInstance(classFulleName, true);
            return GetObjectInstance(fullClassName) as IUserInfoDAL;
        }

	}
	
}