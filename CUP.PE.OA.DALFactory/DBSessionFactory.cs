using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.DALFactory
{
   public static class DBSessionFactory
    {
        public static DBSession GetDBsession()
        {
            if (CallContext.GetData("DBSession")==null)
            {
                CallContext.SetData("DBSession", new DBSession());
            }
            return CallContext.GetData("DBSession") as DBSession;
        }
    }
}
