using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Personnel;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;

namespace Rhea.Business.Personnel
{
    /// <summary>
    /// 指标计算接口
    /// </summary>
    public interface IIndicatorBusiness
    {
        /// <summary>
        /// 部门指标计算
        /// </summary>
        /// <param name="department">部门数据</param>
        /// <returns></returns>
        DepartmentIndicatorModel GetDepartmentIndicator(Department department);

        /// <summary>
        /// 部门指标计算
        /// </summary>
        /// <param name="department">部门数据</param>
        /// <param name="rooms">房间数据</param>
        /// <returns></returns>
        DepartmentIndicatorModel GetDepartmentIndicator(Department department, List<Room> rooms);
    }
}
