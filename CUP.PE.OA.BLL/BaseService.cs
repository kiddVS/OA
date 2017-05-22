using CUP.PE.OA.DALFactory;
using CUP.PE.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.BLL
{
 public abstract  class BaseService<T> where T:class,new ()
    {
      
       public IDBSession CurrentDBSession
        {
            get
            {
                return  DBSessionFactory.GetDBsession();
            }
        }

        public IBaseDAL<T> CurrentDAL { get; set; }
        public BaseService()
        {
            SetCurrentDAL();
        }
        public abstract void SetCurrentDAL();
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDAL.LoadEntities(whereLambda);
        }
        public IQueryable<T> LoadEntitiesByPage<S>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>>
            whereLambda, Expression<Func<T, S>> orderByLambda, bool isAsc)
        {
            return CurrentDAL.LoadEntitiesByPage<S>(pageIndex, pageSize, out total, whereLambda, orderByLambda, isAsc);

        }
        public T AddEntity(T entity)
        {
            CurrentDAL.AddEntity(entity);
            CurrentDBSession.SaveChanges();
            return entity;
        }
        public bool DeleteEntity(T entity)
        {
            CurrentDAL.DeleteEntity(entity);
            return CurrentDBSession.SaveChanges();

        }
        public bool UpdateEntity(T entity)
        {
            CurrentDAL.UpdateEntity(entity);
            return CurrentDBSession.SaveChanges();
        }
    }
}
