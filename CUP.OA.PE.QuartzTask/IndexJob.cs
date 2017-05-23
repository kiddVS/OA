using CUP.PE.OA.BLL;
using CUP.PE.OA.IBLL;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUP.OA.PE.QuartzTask
{
    public class IndexJob : IJob
    {
        IKeyWordsRankService keyWordsRankService = new KeyWordsRankService();
        public void Execute(JobExecutionContext context)
        {
            //定时任务            
            keyWordsRankService.UpdateKeyWordsRank();
        }
    }
}
