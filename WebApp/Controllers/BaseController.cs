using CUP.PE.OA.Common;
using CUP.PE.OA.Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        public UserInfo LoginUser { get; set; }
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            bool isExist = false;
            if (filterContext.HttpContext.Request.Cookies["LoginCookie"] != null)
            {
               object login= MemcachedHelper.GetMemcachedData(filterContext.HttpContext.Request.Cookies["LoginCookie"].Value);
                if (login!=null)
                {
                    LoginUser = JsonSerializeHelper.JsonDeSerialize<UserInfo>(login.ToString());
                    isExist = true;
                    MemcachedHelper.SetMemcachedData(filterContext.HttpContext.Request.Cookies["LoginCookie"].Value, login, DateTime.Now.AddMinutes(20));
                    if (LoginUser.UName == "jc")
                    {
                        return;
                    }
                    string urlPath = Request.Url.AbsolutePath;
                    string httpMethod = Request.HttpMethod.ToUpper();
                    //spring的容器创建业务类的实例
                    IApplicationContext ctx = ContextRegistry.GetContext();
                    CUP.PE.OA.IBLL.IActionInfoService ActionInfoService = (CUP.PE.OA.IBLL.IActionInfoService)ctx.GetObject("ActionInfoService");
                    CUP.PE.OA.IBLL.IUserInfoService UserInfoService = (CUP.PE.OA.IBLL.IUserInfoService)ctx.GetObject("UserInfoService");
                    if (ActionInfoService.LoadEntities(u => u.Url == urlPath && u.HttpMethod == httpMethod).FirstOrDefault() != null)
                    {
                        var userInfo = UserInfoService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault();
                        //1.从用户→权限进行过滤
                        var user2Action = (from u in userInfo.R_UserInfo_ActionInfo
                                           select u).Where(u => u.ActionInfo.Url == urlPath && u.ActionInfo.HttpMethod == httpMethod).FirstOrDefault();
                        if (user2Action != null)
                        {
                            if (user2Action.IsPass == true)
                            {
                                return;
                            }
                            else
                            {
                                filterContext.Result = Redirect("/Error.html");
                            }
                        }
                        else
                        {
                            //2.从用户→角色→权限进行过滤
                            var user_Role_Action = (from u in userInfo.RoleInfo
                                                    from a in u.ActionInfo
                                                    where a.Url == urlPath && a.HttpMethod == httpMethod
                                                    select a).FirstOrDefault();
                            if (user_Role_Action != null)
                            {
                                return;
                            }
                            else
                            {
                                filterContext.Result = Redirect("/Error.html");
                                return;
                            }
                        }
                    }
                    else
                    {
                        filterContext.Result = Redirect("/Error.html");
                        return;
                    }
                }
               
            }
            if (!isExist)
            {
                filterContext.Result = Redirect("/Login/Index");
                return;
            }           
        }
    }
}