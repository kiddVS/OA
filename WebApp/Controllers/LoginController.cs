using CUP.PE.OA.BLL;
using CUP.PE.OA.Common;
using CUP.PE.OA.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        IUserInfoService UserInfoService = new UserInfoService();
        // GET: Login
        public ActionResult Index()
        {
            if (Request.Cookies["UName"]!=null&& !string.IsNullOrEmpty( Request.Cookies["UName"].Value ))
            {
                string cookieUName = Request.Cookies["UName"].Value;
                string cookieUPwd = Request.Cookies["UPwd"].Value;
                var loginUser = UserInfoService.LoadEntities(u => u.UName == cookieUName).FirstOrDefault();
                if (cookieUPwd== MD5Helper.GetMD5String(MD5Helper.GetMD5String(loginUser.UPwd)))
                {
                    string sessionCookie = Guid.NewGuid().ToString();
                    MemcachedHelper.SetMemcachedData(sessionCookie, JsonSerializeHelper.JsonSerialize(loginUser), DateTime.Now.AddMinutes(20));
                    Response.Cookies["LoginCookie"].Value = sessionCookie;
                    Response.Cookies["UName"].Expires = DateTime.Now.AddDays(5);
                    Response.Cookies["UPwd"].Expires = DateTime.Now.AddDays(5);
                    return Redirect("/Home/Index");
                }
                else
                {
                    Response.Cookies["UName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["UPwd"].Expires = DateTime.Now.AddDays(-1);
                }
            }
            return View();
        }
        public ActionResult LoginUser()
        {
            string sessionValidateCode = Session["ValidateCode"] != null ? Session["ValidateCode"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(sessionValidateCode))
            {
                return Content("N:验证码错误！");
            }
           if (!Request["vCode"].Equals(sessionValidateCode,StringComparison.CurrentCultureIgnoreCase))
            {
                return Content("N:验证码错误！");
            }
         
            string uName = Request["LoginCode"];
            string uPwd = Request["LoginPwd"];
            var loginUser = UserInfoService.LoadEntities(u => u.UName == uName && u.UPwd == uPwd).FirstOrDefault();
            if (loginUser==null)
            {
                Session["ValidateCode"] = null;
                return Content("N:用户名密码错误！");
            }
            //  Session["LoginUser"] = loginUser;
            else
            {
                string sessionCookie = Guid.NewGuid().ToString();
                MemcachedHelper.SetMemcachedData(sessionCookie, JsonSerializeHelper.JsonSerialize(loginUser), DateTime.Now.AddMinutes(20));
                Response.Cookies["LoginCookie"].Value = sessionCookie;
               
            }
            if (Request["checkRemember"]!=null)
            {
                HttpCookie cp1 = new HttpCookie("UName", loginUser.UName);
                HttpCookie cp2 = new HttpCookie("UPwd", MD5Helper.GetMD5String(MD5Helper.GetMD5String(loginUser.UPwd)));
                cp1.Expires = DateTime.Now.AddDays(5);
                cp2.Expires = DateTime.Now.AddDays(5);
                HttpContext.Response.Cookies.Add(cp1);
                HttpContext.Response.Cookies.Add(cp2);
            }
            return Content("Y:用户登陆成功！");
        }
        public ActionResult ShowValidateCode()
        {
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }
        public ActionResult Logout()
        {
            if (Request.Cookies["LoginCookie"]!=null)
            {
                if (Request.Cookies["UName"]!=null)
                {
                    Request.Cookies["UName"].Expires = DateTime.Now.AddDays(-1);
                    Request.Cookies["UPwd"].Expires = DateTime.Now.AddDays(-1);
                }
                
                MemcachedHelper.DeleteMemcachedData(Request.Cookies["LoginCookie"].Value);
            }
            return Redirect("/Login/Index");
        }
    }
}