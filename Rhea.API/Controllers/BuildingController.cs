using Rhea.API.Models;
using Rhea.Business.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Rhea.API.Controllers
{
    /// <summary>
    /// 建筑控制器
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BuildingController : ApiController
    {
        #region Field
        /// <summary>
        /// 建筑业务对象
        /// </summary>
        private BuildingBusiness buildingBusiness;
        #endregion //Field

        #region Constructor
        public BuildingController()
        {
            this.buildingBusiness = new BuildingBusiness();
        }
        #endregion //Constructor

        #region Function
        /// <summary>
        /// 模型映射
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Building BindBuilding(Rhea.Model.Estate.Building model)
        {
            if (model == null)
                return null;

            Building building = new Building();
            building.Id = model.BuildingId;
            building.Name = model.Name;
            building.CampusId = model.CampusId;
            building.OrganizeType = model.OrganizeType;
            building.ParentId = model.ParentId;
            building.HasChild = model.HasChild;
            building.UseType = model.UseType;
            building.BuildArea = Convert.ToDouble(model.BuildArea);
            building.LastUpdateTime = model.Log.Time;

            return building;
        }
        #endregion //Function

        #region Action
        /// <summary>
        /// 获取所有建筑数据
        /// </summary>
        /// <returns>所有建筑数据</returns>
        public IEnumerable<Building> Get()
        {
            var data = this.buildingBusiness.Get().OrderBy(r => r.Sort);

            List<Building> buildings = new List<Building>();

            foreach (var item in data)
            {
                buildings.Add(BindBuilding(item));
            }

            return buildings;
        }

        /// <summary>
        /// 获取建筑数据
        /// </summary>
        /// <param name="id">楼宇ID</param>
        /// <returns>建筑数据</returns>
        public Building Get(int id)
        {
            var data = this.buildingBusiness.Get(id);

            if (data == null)
                return null;
            else
            {
                Building building = BindBuilding(data);

                return building;
            }
        }
        #endregion //Action
    }
}
