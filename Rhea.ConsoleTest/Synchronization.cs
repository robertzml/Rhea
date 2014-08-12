using Rhea.Business;
using Rhea.Business.Estate;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Estate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void SyncSubregion()
        {
            SyncBusiness business = new SyncBusiness();

            List<OriginBuildingMap> maps = business.GetRelateData();

            maps = maps.Where(r => r.NewId > 200077 && r.NewId <= 200123).ToList();

            foreach (var row in maps)
            {
                if (row.OrganizeType != 4)
                    continue;
                ErrorCode result = business.SyncSubregionFloor(row);
                Console.WriteLine(result.DisplayName());
            }
        }

        public void SyncBlock()
        {
            SyncBusiness business = new SyncBusiness();

            List<OriginBuildingMap> maps = business.GetRelateData();

            maps = maps.Where(r => r.NewId >= 200124 && r.NewId <= 200196).ToList();

            foreach (var row in maps)
            {
                if (row.OrganizeType != 5)
                    continue;
                ErrorCode result = business.SyncBlockFloor(row);
                Console.WriteLine(result.DisplayName());
            }
        }

        public void SyncCottage()
        {
            SyncBusiness business = new SyncBusiness();

            List<OriginBuildingMap> maps = business.GetRelateData();

            maps = maps.Where(r => r.NewId == 200027).ToList();

            foreach (var row in maps)
            {
                if (row.OrganizeType != 3)
                    continue;
                ErrorCode result = business.SyncCottageFloor(row);
                Console.WriteLine(result.DisplayName());
            }
        }

        public void SyncDepartment()
        {
            SyncBusiness business = new SyncBusiness();

            List<OriginDepartmentMap> maps = business.GetRelateDepartment();

            foreach(var row in maps)
            {
                ErrorCode result = business.SyncDepartment(row);
                Console.WriteLine(result.DisplayName());
            }
        }

        public void SyncRoom()
        {
            SyncBusiness business = new SyncBusiness();

            List<int> roomIds = business.GetOriginRoom();
            List<OriginDepartmentMap> dmaps = business.GetRelateDepartment();
            List<OriginBuildingMap> bmaps = business.GetRelateData();
                       

            foreach (int id in roomIds)
            {
                ErrorCode result = business.SyncRoom(id, bmaps, dmaps);
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

        public void ShowDepartmentMap()
        {
            SyncBusiness business = new SyncBusiness();
            List<OriginDepartmentMap> maps = business.GetRelateDepartment();

            foreach (var row in maps)
            {
                Console.WriteLine("oldId:{0}, newId:{1}, name:{2}, type:{3}", row.OldId, row.NewId, row.NewName, row.Type);
            }
        }

        public void ShowFunctionCode()
        {
            RoomBusiness business = new RoomBusiness();
            List<RoomFunctionCode> functionCodes = business.GetFunctionCodes();

            foreach(var code in functionCodes)
            {
                Console.WriteLine("id:{0}, ft:{1}, st:{2}, rm:{3}", code.CodeId, code.ClassifyName, code.FunctionProperty, code.Remark);
            }
        }
        #endregion //Method
    }
}
