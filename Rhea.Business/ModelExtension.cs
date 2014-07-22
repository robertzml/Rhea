using Rhea.Data.Estate;
using Rhea.Data.Mongo.Estate;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Account;

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
        /// 用户组名称
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <returns></returns>
        public static string UserGroupName(this User user)
        {
            return "Root";
        }
        #endregion //Method
    }
}
