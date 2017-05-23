using CUP.PE.OA.IBLL;
using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.BLL
{
  public partial  class KeyWordsRankService:BaseService<KeyWordsRank>,IKeyWordsRankService
    {
        public int UpdateKeyWordsRank()
        {
            //1.删除热词表
            string sqlDeleteKeyWords = @"truncate table KeyWordsRank";
            CurrentDBSession.ExecuteSql(sqlDeleteKeyWords);
            //2.将热词表重新更新
            string sqlUpdateKeyWordsRank = @"Insert into KeyWordsRank (Id,KeyWords,SearchCount) select
            newid(),SearchDetails.KeyWords,count(*) from  SearchDetails where datediff(day,SearchDetails.SearchDateTime,
            getdate())<=7  group by SearchDetails.KeyWords";
           return CurrentDBSession.ExecuteSql(sqlUpdateKeyWordsRank);
        }
    }
}
