using log4net;
using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApp.Common;
using WebApp.Models;

namespace WebApp
{
    public class MvcApplication : SpringMvcApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //开启线程扫描错误队列，将错误信息写入日志文件中
            ScanExceptionQueue();
            //开启线程扫描Lucene队列
            DataIndexManager.GetInstance().ThreadScanBookQueue();
        }
        private void ScanExceptionQueue()
        {
            string path = Server.MapPath("/Log");
            ThreadPool.QueueUserWorkItem(u =>
            {
                while (true)
                {
                    if (MyExcptionAttribute.ExceptionQueue.Count()!=0)
                    {
                        Exception e = MyExcptionAttribute.ExceptionQueue.Dequeue();

                        // string fullName= path + "/" + DateTime.Now.ToString("yyyy-MM-dd")+".txt";
                        //// Directory.CreateDirectory(fullPath);
                        // File.AppendAllText(fullName, e.ToString(),Encoding.Default);                      
                        ILog logger = LogManager.GetLogger("ExceptionMsg");
                        logger.Error(e.ToString());
                        
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }
            },path);
        }
    }
}
