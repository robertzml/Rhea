using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Business.Personnel;
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

        public static string SubjectTypeName(this Department department)
        {
            IDictionaryBusiness dictionaryBusiness = new PersonnelDictionaryBusiness();
            string name = dictionaryBusiness.GetItemValue("SubjectType", department.SubjectType);
            return name;
        }
    }
}
