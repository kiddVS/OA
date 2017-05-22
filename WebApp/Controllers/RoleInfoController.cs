using CUP.PE.OA.IBLL;
using CUP.PE.OA.Model;
using CUP.PE.OA.Model.EnumType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class RoleInfoController : BaseController
    {
        public IRoleInfoService RoleInfoService { get; set; }
        // GET: RoleInfo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetRoleInfo()
        {
            short isActive = (short)EnumIsActive.ActiveLogical;
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 5 : int.Parse(Request["rows"]);
            int total;
            var entities= RoleInfoService.LoadEntitiesByPage<int>(pageIndex, pageSize, out total, u => u.DelFlag == isActive, u => u.ID, true);
            var pageEntities = from u in entities
                               select new
                               {
                                   ID = u.ID,
                                   RoleName = u.RoleName,
                                   Remark = u.Remark,
                                   SubTime = u.SubTime
                               };
            return Json(new { rows = pageEntities, total = total });
        }
        public ActionResult AddRoleInfo()
        {
            string roleName = Request["RoleName"].ToString();
            string remark = Request["Remark"].ToString();
            string sort = Request["Sort"].ToString();
            DateTime now = DateTime.Now;
            RoleInfo role = new RoleInfo() { RoleName = roleName, Remark = remark, Sort = sort, SubTime = now, ModifiedOn = now, DelFlag = 0 };
            if (RoleInfoService.AddEntity(role)!=null)
            {
                return Content("Y");
            }
            else
            {
                return Content("N:角色添加失败");
            }
        }
        public ActionResult GetEditRoleInfo()
        {
            int id = int.Parse(Request["ID"]);
         var roleInfo= RoleInfoService.LoadEntities(u => u.ID ==id).FirstOrDefault();
            if (roleInfo!=null)
            {
                ViewBag.RoleInfo = roleInfo;
                return View();
            }
            return Content("获取角色信息失败");
        }
        public ActionResult EditRoleInfo(RoleInfo role)
        {
            role.ModifiedOn = DateTime.Now;
            if (RoleInfoService.UpdateEntity(role))
            {
                return Content("Y");
            }
            else
            {
                return Content("N");
            }
        }
        public ActionResult DeleteRoleInfo()
        {
            string roleIDs = Request["IDS"];
            List<int> IDList = new List<int>();
            string[] IDArray = roleIDs.Split(',');
            foreach (var item in IDArray)
            {
                IDList.Add(int.Parse(item));
            }
            if (RoleInfoService.DeleteEntities(IDList))
            {
                return Content("Y");
            }
            else
            {
                return Content("N");
            }
        }
    }
}