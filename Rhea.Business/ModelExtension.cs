using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Business.Account;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
using Rhea.Model.Account;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;

namespace Rhea.Business
{
    public static class ModelExtension
    {
        /// <summary>
        /// 房间所属部门名称
        /// </summary>
        /// <param name="room">房间对象</param>
        /// <returns></returns>
        public static string DepartmentName(this Room room)
        {
            IDepartmentBusiness departmentBusiness = new MongoDepartmentBusiness();
            string name = departmentBusiness.GetName(room.DepartmentId);
            return name;
        }

        /// <summary>
        /// 房间所属楼宇名称
        /// </summary>
        /// <param name="room">房间对象</param>
        /// <returns></returns>
        public static string BuildingName(this Room room)
        {
            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            string name = buildingBusiness.GetName(room.BuildingId);
            return name;
        }

        /// <summary>
        /// 房间所属楼宇楼层总数
        /// </summary>
        /// <param name="room">房间对象</param>
        /// <returns></returns>
        public static int TotalFloor(this Room room)
        {
            IBuildingBusiness buildingBusiness = new MongoBuildingBusiness();
            int total = Convert.ToInt32(buildingBusiness.Get(room.BuildingId).AboveGroundFloor);
            return total;
        }

        /// <summary>
        /// 学科类型名称
        /// </summary>
        /// <param name="department">部门对象</param>
        /// <returns></returns>
        public static string SubjectTypeName(this Department department)
        {
            IDictionaryBusiness dictionaryBusiness = new PersonnelDictionaryBusiness();
            string name = dictionaryBusiness.GetItemValue("SubjectType", department.SubjectType);
            return name;
        }

        /// <summary>
        /// 用户组名称
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public static string UserGroupTitle(this UserProfile user)
        {
            IUserGroupBusiness userGroupBusiness = new MongoUserGroupBusiness();
            string title = userGroupBusiness.Get(user.UserGroupId).Title;
            return title;
        }

        /// <summary>
        /// 楼群所属校区名称
        /// </summary>
        /// <param name="buildingGroup">楼群对象</param>
        /// <returns></returns>
        public static string CampusName(this BuildingGroup buildingGroup)
        {
            ICampusBusiness campusBusiness = new MongoCampusBusiness();
            string name = campusBusiness.GetName(buildingGroup.CampusId);
            return name;
        }
    }
}
