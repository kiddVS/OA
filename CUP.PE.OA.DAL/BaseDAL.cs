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
  public  class BaseDAL<T> where T:class,new()
    {
        DbContext Db = DbContextFactory.GetDbContext();
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().Where(whereLambda);
        }
        public IQueryable<T> LoadEntitiesByPage<S>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> whereLambda
            , Expression<Func<T, S>> orderByLambda, bool isAsc)
        {
            var entities = Db.Set<T>().Where(whereLambda);
            total = entities.Count();
            if (isAsc)
            {
                entities = entities.OrderBy(orderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
   
            }
            else
            {
                entities = entities.OrderByDescending(orderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);               
            }
            return entities;
        }
        public T AddEntity(T entity)
        {
           Db.Set<T>().Add(entity);
            //Db.SaveChanges();
            return entity;
        }
        public bool DeleteEntity(T entity)
        {
            Db.Entry<T>(entity).State = EntityState.Deleted;
            //return Db.SaveChanges() > 0;
            return true;
        }
        public bool UpdateEntity(T entity)
        {
            Db.Entry<T>(entity).State = EntityState.Modified;
            //return Db.SaveChanges() > 0;
            return true;
        }

    }
}
