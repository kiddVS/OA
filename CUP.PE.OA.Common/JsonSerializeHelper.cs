using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.Common
{
   public  class JsonSerializeHelper
    {
        public static string JsonSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T JsonDeSerialize<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
