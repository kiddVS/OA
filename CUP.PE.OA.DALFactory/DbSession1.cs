 

using CUP.PE.OA.DAL;
using CUP.PE.OA.IDAL;
using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.DALFactory
{
	public partial class DBSession : IDBSession
    {
	
		private IActionInfoDAL _ActionInfoDAL;
        public IActionInfoDAL ActionInfoDAL
        {
            get
            {
                if(_ActionInfoDAL == null)
                {
                    _ActionInfoDAL = AbstractFactory.GetActionInfoInstance();;
                }
                return _ActionInfoDAL;
            }
            set { _ActionInfoDAL = value; }
        }
	
		private IBooksDAL _BooksDAL;
        public IBooksDAL BooksDAL
        {
            get
            {
                if(_BooksDAL == null)
                {
                    _BooksDAL = AbstractFactory.GetBooksInstance();;
                }
                return _BooksDAL;
            }
            set { _BooksDAL = value; }
        }
	
		private IDepartmentDAL _DepartmentDAL;
        public IDepartmentDAL DepartmentDAL
        {
            get
            {
                if(_DepartmentDAL == null)
                {
                    _DepartmentDAL = AbstractFactory.GetDepartmentInstance();;
                }
                return _DepartmentDAL;
            }
            set { _DepartmentDAL = value; }
        }
	
		private IR_UserInfo_ActionInfoDAL _R_UserInfo_ActionInfoDAL;
        public IR_UserInfo_ActionInfoDAL R_UserInfo_ActionInfoDAL
        {
            get
            {
                if(_R_UserInfo_ActionInfoDAL == null)
                {
                    _R_UserInfo_ActionInfoDAL = AbstractFactory.GetR_UserInfo_ActionInfoInstance();;
                }
                return _R_UserInfo_ActionInfoDAL;
            }
            set { _R_UserInfo_ActionInfoDAL = value; }
        }
	
		private IRoleInfoDAL _RoleInfoDAL;
        public IRoleInfoDAL RoleInfoDAL
        {
            get
            {
                if(_RoleInfoDAL == null)
                {
                    _RoleInfoDAL = AbstractFactory.GetRoleInfoInstance();;
                }
                return _RoleInfoDAL;
            }
            set { _RoleInfoDAL = value; }
        }
	
		private IUserInfoDAL _UserInfoDAL;
        public IUserInfoDAL UserInfoDAL
        {
            get
            {
                if(_UserInfoDAL == null)
                {
                    _UserInfoDAL = AbstractFactory.GetUserInfoInstance();;
                }
                return _UserInfoDAL;
            }
            set { _UserInfoDAL = value; }
        }
	}	
}