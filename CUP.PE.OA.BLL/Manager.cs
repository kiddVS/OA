 
using CUP.PE.OA.DALFactory;
using CUP.PE.OA.IBLL;
using CUP.PE.OA.IDAL;
using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.BLL
{
	
	public partial class ActionInfoService :BaseService<ActionInfo>,IActionInfoService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL =this.CurrentDBSession.ActionInfoDAL;
        }
    }   
	
	public partial class BooksService :BaseService<Books>,IBooksService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL =this.CurrentDBSession.BooksDAL;
        }
    }   
	
	public partial class DepartmentService :BaseService<Department>,IDepartmentService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL =this.CurrentDBSession.DepartmentDAL;
        }
    }   
	
	public partial class R_UserInfo_ActionInfoService :BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL =this.CurrentDBSession.R_UserInfo_ActionInfoDAL;
        }
    }   
	
	public partial class RoleInfoService :BaseService<RoleInfo>,IRoleInfoService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL =this.CurrentDBSession.RoleInfoDAL;
        }
    }   
	
	public partial class UserInfoService :BaseService<UserInfo>,IUserInfoService
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL =this.CurrentDBSession.UserInfoDAL;
        }
    }   
	
}