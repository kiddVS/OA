using CUP.PE.OA.IBLL;
using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.BLL
{
     public partial  class ActionInfoService:BaseService<ActionInfo>,IActionInfoService
    {
        public  bool SetRole4Action(int actionID, List<int> roleIDList)
        {
            var actionInfo = CurrentDBSession.ActionInfoDAL.LoadEntities(u => u.ID == actionID).FirstOrDefault();
            actionInfo.RoleInfo.Clear();
            foreach (var item in roleIDList)
            {
                var roleInfo = CurrentDBSession.RoleInfoDAL.LoadEntities(u => u.ID == item).FirstOrDefault();
                actionInfo.RoleInfo.Add(roleInfo);
            }
            return CurrentDBSession.SaveChanges();
        }
    }
}
