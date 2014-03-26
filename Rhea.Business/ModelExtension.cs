using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Estate;
using Rhea.Business.Estate;

namespace Rhea.Business
{
    /// <summary>
    /// 类扩展方法集合
    /// </summary>
    public static class ModelExtension
    {
        #region Method
        /// <summary>
        /// 楼群所属校区名称
        /// </summary>
        /// <param name="buildingGroup">楼群对象</param>
        /// <returns></returns>
        public static string CampusName(this BuildingGroup buildingGroup)
        {
            CampusBusiness campusBusiness = new CampusBusiness();
            var campus = campusBusiness.Get(buildingGroup.CampusId);
            if (campus != null)
                return campus.Name;
            else
                return string.Empty;
        }
        #endregion //Method
    }
}
