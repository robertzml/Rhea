using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Data.Mongo
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
        /// 字典表
        /// dictionary
        /// </summary>
        public static readonly string Dictionary = "dictionary";
     
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
        #endregion //Field
    }
}
