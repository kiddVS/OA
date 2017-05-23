 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CUP.PE.OA.IDAL
{
	public partial interface IDBSession
    {

	
		IActionInfoDAL ActionInfoDAL{get;set;}
	
		IBooksDAL BooksDAL{get;set;}
	
		IDepartmentDAL DepartmentDAL{get;set;}
	
		IKeyWordsRankDAL KeyWordsRankDAL{get;set;}
	
		IR_UserInfo_ActionInfoDAL R_UserInfo_ActionInfoDAL{get;set;}
	
		IRoleInfoDAL RoleInfoDAL{get;set;}
	
		ISearchDetailsDAL SearchDetailsDAL{get;set;}
	
		IUserInfoDAL UserInfoDAL{get;set;}
        DbContext Db { get; }
        bool SaveChanges();
	}	
}