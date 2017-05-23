using CUP.PE.OA.DAL;
using CUP.PE.OA.IDAL;
using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.DALFactory
{
  public partial class DBSession:IDBSession
    {
       public DbContext Db
        {
            get
            {
                return DbContextFactory.GetDbContext();
            }
        }
        //private IUserInfoDAL _userInfoDAL;
        //public IUserInfoDAL UserInfoDAL
        //{
        //    get
        //    {
        //        if (_userInfoDAL==null )
        //        {
        //            _userInfoDAL = AbstractFactory.GetUserInfoInstance();
        //        }
        //        return _userInfoDAL;
        //    }
        //    set
        //    {
        //        _userInfoDAL = value;
        //    }
        //}
        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }
        //执行sql的DB方法
       
        public int ExecuteSql( string sql,params SqlParameter[] sqlParas)
        {
           return  Db.Database.ExecuteSqlCommand(sql, sqlParas);
        }
    }
}
