using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Data
{
    /// <summary>
    /// 房产Collection名称
    /// </summary>
    public static class EstateCollection
    {
        #region Field
        /// <summary>
        /// 校区表
        /// campus
        /// </summary>
        public static readonly string Campus = "campus";
      
        /// <summary>
        /// 建筑表
        /// building
        /// </summary>
        public static readonly string Building = "building";

        /// <summary>
        /// 房间表
        /// room
        /// </summary>
        public static readonly string Room = "room";   
     
        /// <summary>
        /// 校区备份表
        /// campusBackup
        /// </summary>
        public static readonly string CampusBackup = "campusBackup";

        /// <summary>
        /// 建筑备份表
        /// buildingBackup
        /// </summary>
        public static readonly string BuildingBackup = "buildingBackup";

        /// <summary>
        /// 房间备份表
        /// roomBackup
        /// </summary>
        public static readonly string RoomBackup = "roomBackup";

        /// <summary>
        /// 房间归档表
        /// roomArchive
        /// </summary>
        public static readonly string RoomArchive = "roomArchive";

        /// <summary>
        /// 青教住户表
        /// inhabitant
        /// </summary>
        public static readonly string Inhabitant = "inhabitant";

        /// <summary>
        /// 青教入住记录表
        /// resideRecord
        /// </summary>
        public static readonly string ResideRecord = "resideRecord";

        /// <summary>
        /// 青教业务记录表
        /// apartmentTransaction
        /// </summary>
        public static readonly string ApartmentTransaction = "apartmentTransaction";
        #endregion //Field
    }
}
