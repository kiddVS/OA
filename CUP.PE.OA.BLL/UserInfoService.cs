using CUP.PE.OA.DALFactory;
using CUP.PE.OA.IBLL;
using CUP.PE.OA.IDAL;
using CUP.PE.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CUP.PE.OA.BLL
{
  public  partial class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {
        //public override void SetCurrentDAL()
        //{
        //    this.CurrentDAL = this.CurrentDBSession.UserInfoDAL;
        //}
        public bool DeleteEntities(List<int> IDList)
        {
            var entities = CurrentDBSession.UserInfoDAL.LoadEntities(u => IDList.Contains(u.ID));
            foreach (var entity in entities)
            {
                CurrentDBSession.UserInfoDAL.DeleteEntity(entity);
            }           
            return CurrentDBSession.SaveChanges();
        }
        public IQueryable<UserInfo> GetSearchUserInfo(SearchUserInfo searchUserInfo, CUP.PE.OA.Model.EnumType.EnumIsActive enumFlag)
        {
            short delFlag = (short)enumFlag;
            var entities = CurrentDBSession.UserInfoDAL.LoadEntities(u => u.DelFlag == delFlag);
            if (!string.IsNullOrEmpty(searchUserInfo.SearchUName))
            {
                entities = entities.Where(u => u.UName.Contains(searchUserInfo.SearchUName));
            }
            if (!string.IsNullOrEmpty(searchUserInfo.SearchRemark))
            {
                entities = entities.Where(u => u.Remark.Contains(searchUserInfo.SearchRemark));
            }
            searchUserInfo.Total = entities.Count();
            return entities.OrderBy<UserInfo, int>(u => u.ID).Skip((searchUserInfo.PageIndex - 1) * searchUserInfo.PageSize).Take(searchUserInfo.PageSize);
        }
        public bool SetUserRole(int ID,List<int> roleIDlist)
        {
            var user = CurrentDBSession.UserInfoDAL.LoadEntities(u => u.ID == ID).FirstOrDefault();
            user.RoleInfo.Clear();
            var role = CurrentDBSession.RoleInfoDAL.LoadEntities(u => roleIDlist.Contains(u.ID));
            foreach (var item in role)
            {
                user.RoleInfo.Add(item);
            }
            return CurrentDBSession.SaveChanges();
        }
        public int SetAction4User(int userID, int actionID, string handlerType)
        {
            //0:失败
            //1:设置成功
            //2:清除成功
          //  var userInfo = CurrentDBSession.UserInfoDAL.LoadEntities(u => u.ID == userID).FirstOrDefault();
            var R_action_userInfo = CurrentDBSession.R_UserInfo_ActionInfoDAL.LoadEntities(u => u.UserInfoID == userID && u.ActionInfoID==actionID).FirstOrDefault();
           // var existAction = userInfo.R_UserInfo_ActionInfo.Where(u => u.ID == actionID).FirstOrDefault();
            if (handlerType=="F")
            {
                if (R_action_userInfo != null)
                {
                    CurrentDBSession.R_UserInfo_ActionInfoDAL.DeleteEntity(R_action_userInfo);
                    // userInfo.R_UserInfo_ActionInfo.Remove(R_action_userInfo);
                    return CurrentDBSession.SaveChanges() == true ? 2 : 0;
                }
                else
                {
                    return 0;
                }
            }
            if (handlerType=="Y"||handlerType=="N")
            {
                if (R_action_userInfo != null)
                {
                    R_action_userInfo.IsPass = handlerType == "Y" ? true : false;
                    return CurrentDBSession.SaveChanges() == true ? 1 : 0;
                }
                else
                {
                    R_UserInfo_ActionInfo entity = new R_UserInfo_ActionInfo();
                    entity.UserInfoID = userID;
                    entity.ActionInfoID = actionID;
                    entity.IsPass = handlerType == "Y" ? true : false;
                     CurrentDBSession.R_UserInfo_ActionInfoDAL.AddEntity(entity) ;
                    return CurrentDBSession.SaveChanges() == true ? 1 : 0;
                }
              
            }
            else
            {
                return 0;
            }
        }
        //IDBSession CurrentDBSession = new DBSesRsion();
        //public IQueryable<UserInfo> LoadEntities(Expression<Func<UserInfo,bool>> whereLambda)
        //{
        //   return CurrentDBSession.UserInfoDAL.LoadEntities(whereLambda);
        //}
        //public IQueryable<UserInfo> LoadEntitiesByPage<S>(int pageIndex,int pageSize,out int total,Expression<Func<UserInfo,bool>>
        //    whereLambda,Expression<Func<UserInfo,S>> orderByLambda,bool isAsc)
        //{
        //    return CurrentDBSession.UserInfoDAL.LoadEntitiesByPage<S>(pageIndex, pageSize, out total, whereLambda, orderByLambda, isAsc);

        //}
        //public UserInfo AddEntity(UserInfo entity)
        //{
        //    CurrentDBSession.UserInfoDAL.AddEntity(entity);
        //    CurrentDBSession.SaveChanges();
        //    return entity;
        //}
        //public bool DeleteAntity(UserInfo entity)
        //{
        //    CurrentDBSession.UserInfoDAL.DeleteEntity(entity);
        //    return CurrentDBSession.SaveChanges();

        //}
        //public bool UpdateEntity(UserInfo entity)
        //{
        //    CurrentDBSession.UserInfoDAL.UpdateEntity(entity);
        //    return CurrentDBSession.SaveChanges();
        //}
    }
}
