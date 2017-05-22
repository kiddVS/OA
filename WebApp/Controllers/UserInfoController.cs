using CUP.PE.OA.BLL;
using CUP.PE.OA.Common;
using CUP.PE.OA.IBLL;
using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class UserInfoController : BaseController
    {

        // GET: UserInfo
        IUserInfoService UserInfoService { get; set; }
        IRoleInfoService RoleInfoService { get; set; }
        IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService { get; set; }
        IActionInfoService ActionInfoService { get; set; }
        public ActionResult Index()
        {            
            return View();
        }
        public ActionResult GetUserInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 5 : int.Parse(Request["rows"]);
            string searchUName = Request["searchUName"];
            string searchRemark = Request["searchRemark"];
            SearchUserInfo search = new SearchUserInfo() { PageIndex = pageIndex, PageSize = pageSize, SearchRemark = searchRemark, SearchUName = searchUName };
            //int total;
            CUP.PE.OA.Model.EnumType.EnumIsActive enumDel = CUP.PE.OA.Model.EnumType.EnumIsActive.ActiveLogical;
            var entities = UserInfoService.GetSearchUserInfo(search, enumDel);
            //var entities = userInfoService.LoadEntitiesByPage<int>(pageIndex, pageSize, out total, u => u.DelFlag == enumDel, u => u.ID, true);
            var showEntities = from u in entities
                               select new
                               {
                                   ID = u.ID,
                                   UName = u.UName,
                                   UPwd = u.UPwd,
                                   Remark = u.Remark,
                                   SubTime = u.SubTime
                               };
            var json = new { total = search.Total, rows = showEntities };
            return Json(json);
        }
        public ActionResult DeleteUserInfo()
        {
            string strID = Request["IDS"] ?? "";
            if (string.IsNullOrEmpty(strID))
            {
                return Content("N");
            }
            List<int> IDList = new List<int>();
            string[] idArr = strID.Split(',');
            for (int i = 0; i < idArr.Length; i++)
            {
                IDList.Add(int.Parse(idArr[i]));

            }
            if (UserInfoService.DeleteEntities(IDList))
            {
                return Content("Y");
            }
            return Content("N");
        }
        public ActionResult AddUserInfo(UserInfo entity)
        {
            entity.DelFlag = 0;
            entity.ModifiedOn = DateTime.Now;
            entity.SubTime = DateTime.Now;
            UserInfoService.AddEntity(entity);
            return Content("Y");
        }
        public ActionResult GetEditUserInfo()
        {
            int ID = int.Parse(Request["ID"]);
            var entity = UserInfoService.LoadEntities(u => u.ID == ID).FirstOrDefault();
            string json= (JsonSerializeHelper.JsonSerialize(entity));
            return Content(json);
         //   return Json(entity, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditUserInfo(UserInfo entity)
        {
            entity.ModifiedOn = DateTime.Now;
            UserInfoService.UpdateEntity(entity);
            return Content("Y");
        }
        public ActionResult SetUserRole()
        {
            int userID = int.Parse(Request["ID"]);
            short delFlag = (short)CUP.PE.OA.Model.EnumType.EnumIsActive.ActiveLogical;
            var userInfo = UserInfoService.LoadEntities(u => u.ID == userID).FirstOrDefault();
            var allRoleInfo = RoleInfoService.LoadEntities(u => u.DelFlag == delFlag ).ToList();
            var userRoleInfo = (from u in userInfo.RoleInfo
                                select u.ID).ToList();
            ViewBag.UserInfo = userInfo;
            ViewBag.AllRoleInfo = allRoleInfo;
            ViewBag.UserRoleInfo = userRoleInfo;
            return View();
        }
       public ActionResult SetRoleExecute()
        {
            int ID = int.Parse(Request["ID"]);
            string[] roleIDS = Request.Form.AllKeys;
            List<int> roleIDList = new List<int>();
            foreach (var item in roleIDS)
            {
                if (item.Contains("pe_"))
                {
                    roleIDList.Add(int.Parse(item.Replace("pe_", "")));
                }
               
            }
            if (UserInfoService.SetUserRole(ID, roleIDList))
            {
                return Content("Y");
            }
            else
            {
                return Content("N");
            }
        }
        #region 显示特殊用户的权限
        public ActionResult ShowUserAction()
        {
            int userID = int.Parse(Request["ID"]);
            var userInfo = UserInfoService.LoadEntities(u => u.ID == userID).FirstOrDefault();
            var userExistAction = userInfo.R_UserInfo_ActionInfo.ToList();
            short delFlag=(short)(short)CUP.PE.OA.Model.EnumType.EnumIsActive.ActiveLogical;
            var allActionInfo = ActionInfoService.LoadEntities(u => u.DelFlag == delFlag).ToList();
            ViewBag.UserInfo = userInfo;
            ViewBag.AllActionInfo = allActionInfo;
            ViewBag.UserExistAction = userExistAction;
            return View();
        }
        #endregion
        #region 为特殊用户设置权限
        public ActionResult SetAction4User()
        {
            int userID = int.Parse(Request["USerID"]);
            int actionID = int.Parse(Request["ActionID"]);
            string handlerType = Request["Type"];
            
            return Content(UserInfoService.SetAction4User(userID, actionID, handlerType).ToString());           
          
        }
        #endregion
    }
}