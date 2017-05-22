using CUP.PE.OA.IBLL;
using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.BLL
{
    public partial  class RoleInfoService:BaseService<RoleInfo>,IRoleInfoService
    {
        public bool DeleteEntities(List<int> IDList)
        {
            var entities = CurrentDBSession.RoleInfoDAL.LoadEntities(u => IDList.Contains(u.ID));
            foreach (var entity in entities)
            {
                CurrentDBSession.RoleInfoDAL.DeleteEntity(entity);
            }
            return CurrentDBSession.SaveChanges();
        }
    }
}
