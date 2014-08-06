using Rhea.Data.Account;
using Rhea.Data.Estate;
using Rhea.Data.Mongo.Account;
using Rhea.Data.Mongo.Estate;
using Rhea.Model.Account;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Business
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ModelExtension
    {
        #region Method
        /// <summary>
        /// 建筑所属校区名称
        /// </summary>
        /// <param name="building">建筑对象</param>
        /// <returns></returns>
        public static string CampusName(this Building building)
        {
            ICampusRepository campusRepository = new MongoCampusRepository();
            string name = campusRepository.Get(building.CampusId).Name;
            return name;
        }

        /// <summary>
        /// 父级建筑名称
        /// </summary>
        /// <param name="building">建筑对象</param>
        /// <returns></returns>
        public static string ParentName(this Building building)
        {
            if (building.ParentId == 200000)
                return RheaConstant.TopParentBuildingName;

            IBuildingRepository repository = new MongoBuildingRepository();
            string name = repository.Get(building.ParentId).Name;
            return name;
        }

        /// <summary>
        /// 房间所属建筑名称
        /// </summary>
        /// <param name="room">房间对象</param>
        /// <returns></returns>
        public static string BuildingName(this Room room)
        {
            IBuildingRepository repository = new MongoBuildingRepository();
            string name = repository.Get(room.BuildingId).Name;
            return name;
        }

        /// <summary>
        /// 房间所属部门名称
        /// </summary>
        /// <param name="room">房间对象</param>
        /// <returns></returns>
        public static string DepartmentName(this Room room)
        {
            //IDep

            return null;
        }

        /// <summary>
        /// 用户组名称
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public static string UserGroupName(this User user)
        {
            IUserGroupRepository repository = new MongoUserGroupRepository();
            string name = repository.Get(user.UserGroupId).Name;
            return name;
        }
        #endregion //Method
    }
}
