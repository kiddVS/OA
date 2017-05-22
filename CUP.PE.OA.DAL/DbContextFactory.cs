using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.DAL
{
   public static class DbContextFactory
    {
        public static DbContext GetDbContext()
        {
            if (CallContext.GetData("DbContext")==null)
            {
                CallContext.SetData("DbContext", new OAEntities());
            }
            return CallContext.GetData("DbContext") as DbContext;
        }
    }
}
