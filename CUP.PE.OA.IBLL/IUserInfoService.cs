using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.IBLL
{
  public partial  interface IUserInfoService:IBaseService<UserInfo>
    {
        bool  SetUserRole( int ID,List<int> roleIDList);
        bool DeleteEntities(List<int> IDList);
        IQueryable<UserInfo> GetSearchUserInfo(SearchUserInfo searchUserInfo, CUP.PE.OA.Model.EnumType.EnumIsActive enumFlag);
       int SetAction4User(int userID, int actionID, string handlerType);
    }
}
