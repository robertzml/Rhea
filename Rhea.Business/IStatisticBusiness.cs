using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Estate;
using Rhea.Model.Estate;

namespace Rhea.Business
{
    /// <summary>
    /// 统计业务接口
    /// </summary>
    public interface IStatisticBusiness
    {
        /// <summary>
        /// 获取部门二级分类面积
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="firstCode">一级编码</param>
        /// <param name="functionCodes">功能编码列表</param>
        List<DepartmentSecondClassifyAreaModel> GetSecondClassifyArea(int departmentId, int firstCode, List<RoomFunctionCode> functionCodes);


        /// <summary>
        /// 获取部门一级级分类面积
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="firstCode">一级编码</param>
        /// <param name="title">编码名称</param>
        /// <param name="functionCodes">功能编码列表</param>
        /// <returns></returns>
        DepartmentFirstClassifyAreaModel GetFirstClassifyArea(int departmentId, int firstCode, string title, List<RoomFunctionCode> functionCodes);

        /// <summary>
        /// 获取部门分类面积
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="sortByFirstArea">是否按一级分类排序</param>
        /// <returns></returns>
        DepartmentClassifyAreaModel GetDepartmentClassifyArea(int departmentId, bool sortByFirstArea = true);     
       
        /// <summary>
        /// 根据类别得到楼群建筑面积
        /// </summary>
        /// <param name="useType">楼宇使用类型</param>
        /// <returns></returns>
        double GetBuildingAreaByType(int buildingType);

         /// <summary>
        /// 获取部门总面积模型
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        DepartmentTotalAreaModel GetDepartmentTotalArea(int departmentId);
    }
}
