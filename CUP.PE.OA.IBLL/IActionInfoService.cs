﻿using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.IBLL
{
     public partial interface IActionInfoService:IBaseService<ActionInfo>
    {
        bool SetRole4Action(int actionID, List<int> roleIDList);
    }
}
