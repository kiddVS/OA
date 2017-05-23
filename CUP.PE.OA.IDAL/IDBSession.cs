using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.IDAL
{
  public partial interface IDBSession
    {
        // DbContext Db { get; }
        //  IUserInfoDAL UserInfoDAL { get; set; }
        // bool SaveChanges();
        int ExecuteSql(string sql, params SqlParameter[] sqlParas);
    }
}
