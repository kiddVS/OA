using CUP.PE.OA.BLL;
using CUP.PE.OA.IBLL;
using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : BaseController
    {
        IUserInfoService userInfoService = new UserInfoService();
        public ActionResult Index()
        {
            ViewBag.LoginUser= this.LoginUser;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #region 获取用户的菜单权限
        public ActionResult GetUserMenuAction()
        {
            var userInfo = userInfoService.LoadEntities(U => U.ID == LoginUser.ID).FirstOrDefault();
            //1.根据用户→角色→权限得到权限
            var userRoleAction = (from u in userInfo.RoleInfo
                                  from a in u.ActionInfo
                                  where a.ActionTypeEnum == 1
                                  select a).ToList();
            //var userRole_Action=(from u in userInfo.RoleInfo).Where() 
            //2.根据用户→中间表→权限表查询
            var userToAction = (from u in userInfo.R_UserInfo_ActionInfo
                                where u.ActionInfo.ActionTypeEnum == 1
                                select u.ActionInfo).ToList();
            //var usr2Action = userInfo.R_UserInfo_ActionInfo.Where(u => u.ActionInfo.ActionTypeEnum == 1).Select(u => u.ActionInfo).ToList();
            //3.将两个集合合并
            userToAction.AddRange(userRoleAction);
            //4.查询用户→权限中被禁止的权限
            var forbidAction = (from u in userInfo.R_UserInfo_ActionInfo
                               where u.IsPass == false
                               select u.ActionInfoID).ToList();
            //var fobid = userInfo.R_UserInfo_ActionInfo.Where(u => u.IsPass == true).Select(u => u.ActionInfo).ToList();
            //5.去除禁止的权限
            //foreach (var item in userToAction)
            //{
            //    for (int i = 0;i< forbidAction.Count(); i++)
            //    {
            //        if (item.ID==forbidAction[i].ID)
            //        {
            //            userToAction.Remove(item);
            //        }
            //    }
            //}
            var allUserAction=userToAction.Where(u => !forbidAction.Contains(u.ID));
            //6.去除重复
            allUserAction= allUserAction.Distinct(new CompareAction());
            var json = from u in allUserAction // icon: '../../Content/Images/3DSMAX.png', title: '用户管理', url: '/UserInfo/Index'-
                       select new
                       {
                           icon = u.MenuIcon,
                           title = u.ActionInfoName,
                           url = u.Url,
                       };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}