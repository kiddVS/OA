using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.Common
{
  public  class MemcachedHelper
    {
        private static readonly MemcachedClient MemcachedClient = null;
        static MemcachedHelper()
        {
            string[] servers = { "127.0.0.1:11211", "192.168.202.128:11211" };

            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            //客户端实例
            MemcachedClient = new MemcachedClient();
        }
        public static void SetMemcachedData(string key,object value)
        {
            MemcachedClient.Set(key, value);
        }
        public static void SetMemcachedData(string key,object value,DateTime expire)
        {
            MemcachedClient.Set(key, value, expire);
        }
        public static object GetMemcachedData(string key)
        {
            return MemcachedClient.Get(key);
        }
        public static bool DeleteMemcachedData(string key)
        {
            return MemcachedClient.Delete(key);
        }
    }
}
