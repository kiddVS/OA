using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.IBLL
{
    public partial interface IRoleInfoService : IBaseService<RoleInfo>
    {
        bool DeleteEntities(List<int> IDList);
    }
}
