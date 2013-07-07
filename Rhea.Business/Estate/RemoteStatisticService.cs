using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 统计业务类
    /// </summary>
    public class RemoteStatisticService : IStatisticService
    {
        #region Method
        /// <summary>
        /// 获取统计面积数据
        /// </summary>
        /// <param name="type">统计类型</param>
        /// <remarks>
        /// type=1:学院分类用房面积
        /// type=2:学院分楼宇用房面积
        /// </remarks>
        /// <returns></returns>
        public T GetStatisticArea<T>(int type)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Statistic?type=" + type.ToString());

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<T>().Result;
                return data;
            }
            else
                return default(T);
        }

        /// <summary>
        /// 获取对象数量
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public int GetEntitySize(int type)
        {
            ApiService api = new ApiService();
            HttpResponseMessage response = api.Get("Statistic?type=" + type.ToString());

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsAsync<int>().Result;
                return data;
            }
            else
                return 0;
        }

        public List<Data.Estate.SecondClassifyAreaModel> GetClassifyArea(int departmentId, int firstCode, List<RoomFunctionCode> functionCodes)
        {
            throw new NotImplementedException();
        }

        public Data.Estate.CollegeClassifyAreaModel GetCollegeClassifyArea(int departmentId)
        {
            throw new NotImplementedException();
        }      

        public List<Data.Estate.BuildingAreaModel> GetBuildingArea(int departmentId, List<Building> buildingList)
        {
            throw new NotImplementedException();
        }

        public Data.Estate.CollegeBuildingAreaModel GetCollegeBuildingArea(int departmentId)
        {
            throw new NotImplementedException();
        }

        public double GetBuildingAreaByType(int buildingType)
        {
            throw new NotImplementedException();
        }
        #endregion //Method        
    }
}
