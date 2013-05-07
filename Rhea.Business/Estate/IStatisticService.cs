﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Entities;
using Rhea.Data.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 统计业务接口
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// 获取部门二级分类面积
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="firstCode">一级编码</param>
        /// <param name="functionCodes">功能编码列表</param>
        List<SecondClassifyAreaModel> GetClassifyArea(int departmentId, int firstCode, List<RoomFunctionCode> functionCodes);

        /// <summary>
        /// 得到部门分类面积
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        CollegeClassifyAreaModel GetCollegeClassifyArea(int departmentId);

        /// <summary>
        /// 获取部门楼宇面积
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="buildingList">楼宇列表</param>
        /// <returns></returns>
        List<BuildingAreaModel> GetBuildingArea(int departmentId, List<Building> buildingList);

        /// <summary>
        /// 得到部门分楼宇面积
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        CollegeBuildingAreaModel GetCollegeBuildingArea(int departmentId);
    }
}
