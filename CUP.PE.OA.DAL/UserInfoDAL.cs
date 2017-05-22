using CUP.PE.OA.IDAL;
using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.DAL
{
    public partial class UserInfoDAL:BaseDAL<UserInfo>,IUserInfoDAL
    {
        //OAEntities Db = new OAEntities();
        //public IQueryable<UserInfo> LoadEntities(Expression<Func<UserInfo, bool>> whereLambda)
        //{
        //    return Db.UserInfo.Where(whereLambda);
        //}
        //public IQueryable<UserInfo> LoadEntitiesByPage<S>(int pageIndex, int pageSize, out int total, Expression<Func<UserInfo, bool>> whereLambda
        //    , Expression<Func<UserInfo, S>> orderByLambda, bool isAsc)
        //{
        //    var entities = Db.UserInfo.Where(whereLambda);
        //    total = entities.Count();
        //    if (isAsc)
        //    {
        //        entities = entities.OrderBy(orderByLambda);
        //        return entities.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //    }
        //    else
        //    {
        //        entities = entities.OrderByDescending(orderByLambda);
        //        return entities.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //    }

        //}
        //public UserInfo AddEntity(UserInfo entity)
        //{
        //    Db.UserInfo.Add(entity);
        //    Db.SaveChanges();
        //    return entity;
        //}
        //public bool DeleteEntity (UserInfo entity)
        //{
        //    Db.Entry<UserInfo>(entity).State = EntityState.Deleted;
        //    return Db.SaveChanges() > 0;
        //}
        //public bool UpdateEntity(UserInfo entity)
        //{
        //    Db.Entry<UserInfo>(entity).State = EntityState.Modified;
        //    return Db.SaveChanges() > 0;
        //}

    }
}

