using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.Common
{
  public   class MD5Helper
    {
        public static string GetMD5String(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            buffer = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (var item in buffer)
            {
                sb.Append(item.ToString("X2"));
            }
            md5.Clear();
            return sb.ToString();
        }
    }
}
