using CUP.PE.OA.BLL;
using CUP.PE.OA.IBLL;
using CUP.PE.OA.Model;
using CUP.PE.OA.Model.EnumType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ActionInfoController : BaseController
    {
        IActionInfoService ActionInfoService { get; set; }
        IRoleInfoService RoleInfoService { get; set; }
        // GET: ActionInfo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetActionInfo()
        {
            short isActive = (short)EnumIsActive.ActiveLogical;
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 5 : int.Parse(Request["rows"]);
            int total;
            var entities = ActionInfoService.LoadEntitiesByPage<int>(pageIndex, pageSize, out total, u => u.DelFlag == isActive, u => u.ID, true);
            var pageEntities = from u in entities
                               select new
                               {
                                   ID = u.ID,
                                   ActionInfoName = u.ActionInfoName,
                                   Remark = u.Remark,
                                   SubTime = u.SubTime,
                                   Url = u.Url,
                                   HttpMethod = u.HttpMethod,
                                   ActionTypeEnum = u.ActionTypeEnum,
                                   Sort = u.Sort
                               };
            return Json(new { rows = pageEntities, total = total }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ActionFileUpLoad()
        { 
           
            HttpPostedFileBase file = Request.Files["imgFile"];
            string extFileName = Path.GetExtension(file.FileName);
            if (extFileName == ".jpg"|| extFileName == ".gif"||extFileName == ".png")
            {
                string path = "/ImageIcon/" + DateTime.Now.ToString("yyyy-MM-dd");
                Directory.CreateDirectory(Server.MapPath(path));
                string newFileImgName= Guid.NewGuid() + extFileName;
                string fullImgName = path + "/" + newFileImgName;
                file.SaveAs(Server.MapPath(fullImgName));
                return Content("Y:"+ fullImgName);
            }
            else
            {
                return Content("N");
            }
        }
        public ActionResult AddActionInfo()
        {
            ActionInfo action = new ActionInfo();
            action.ActionInfoName = Request["ActionInfoName"];
            action.ActionTypeEnum =short.Parse( Request["ActionTypeEnum"]);
            action.Remark = Request["Remark"];
            action.Sort = Request["Sort"];
            action.MenuIcon = Request["MenuIcon"];
            action.Url = Request["Url"];
            action.HttpMethod = Request["HttpMethod"];
            action.DelFlag = 0;
            action.SubTime = DateTime.Now;
            action.ModifiedOn = DateTime.Now.ToString();           
            if (ActionInfoService.AddEntity(action)!=null)
            {
                return Content("Y");
            }
            else
            {
                return Content("N");
            }
        }
        #region 展示权限所具有的所有角色
        public ActionResult ShowActionRoleInfo()
        {
            int actionID = int.Parse(Request["ID"]);
            var actionInfo = ActionInfoService.LoadEntities(u => u.ID == actionID).FirstOrDefault();
           // List<int> roleListIDS = new List<int>();
            var roleListIDS = (from u in actionInfo.RoleInfo
                         select u.ID).ToList();
            short delFlag=(short)EnumIsActive.ActiveLogical;
            var allRoles = RoleInfoService.LoadEntities(u => u.DelFlag == delFlag).ToList();
            ViewBag.ActionInfo = actionInfo;
            ViewBag.RoleInAction = roleListIDS;
            ViewBag.AllRoles = allRoles;
            return View();            
        }
        #endregion
        #region 为权限设置角色
        public ActionResult SetRole4Action()
        {
            int actionID = int.Parse(Request["ID"]);
            string[] roleIDS = Request.Form.AllKeys;            
            List<int> roleIDList = new List<int>();
            foreach (var item in roleIDS)
            {
                if (item.StartsWith("pe_"))
                {
                    roleIDList.Add(int.Parse(item.Replace("pe_","")));
                }              
            }
            if (ActionInfoService.SetRole4Action(actionID,roleIDList))
            {
                return Content("Y");
            }
            else
            {
                return Content("N");
            }
        }
        #endregion
    }
}