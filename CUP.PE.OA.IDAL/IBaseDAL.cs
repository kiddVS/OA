using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.IDAL
{
   public interface IBaseDAL<T> where T:class,new()
    {
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> LoadEntitiesByPage<S>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> whereLambda
            , Expression<Func<T, S>> orderByLambda, bool isAsc);
        T AddEntity(T entity);
        bool DeleteEntity(T entity);
        bool UpdateEntity(T entity);
    }
}
