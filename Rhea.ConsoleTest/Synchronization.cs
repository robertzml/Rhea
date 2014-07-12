using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Business;
using Rhea.Model;
using Rhea.Model.Estate;
using Rhea.Common;

namespace Rhea.ConsoleTest
{
    /// <summary>
    /// 同步原始数据
    /// </summary>
    public class Synchronization
    {
        #region Method
        public void SyncBuildingGroup()
        {
            SyncBusiness business = new SyncBusiness();
            List<OriginBuildingMap> maps = business.GetRelateData();

            maps = maps.Where(r => r.NewId >= 200038 && r.NewId <= 200076).ToList();

            foreach(var row in maps)
            {
                ErrorCode result = business.SyncBuildingGroup(row);
                Console.WriteLine(result.DisplayName());
            }
        }

        public void SyncBuilding()
        {
            SyncBusiness business = new SyncBusiness();
            List<OriginBuildingMap> maps = business.GetRelateData();

            maps = maps.Where(r => r.NewId >= 200077 && r.NewId <= 200196).ToList();

            foreach (var row in maps)
            {
                ErrorCode result = business.SyncBuilding(row);
                Console.WriteLine(result.DisplayName());
            }
        }

        public void ShowBuildingMap()
        {
            SyncBusiness business = new SyncBusiness();
            List<OriginBuildingMap> maps = business.GetRelateData();

            foreach(var row in maps)
            {
                Console.WriteLine("newId:{0}, oldId:{1}, parentId:{2}, type:{3}", row.NewId, row.OldId, row.NewParentId, row.OrganizeType);
            }
        }
        #endregion //Method
    }
}
