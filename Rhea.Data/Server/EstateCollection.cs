using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhea.Data.Server
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
        /// 楼群表
        /// buildingGroup
        /// </summary>
        public static readonly string BuildingGroup = "buildingGroup";

        /// <summary>
        /// 楼宇表
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
        /// 杂项表
        /// misc
        /// </summary>
        public static readonly string Misc = "misc";

        /// <summary>
        /// 楼群备份表
        /// buildingGroupBackup
        /// </summary>
        public static readonly string BuildingGroupBackup = "buildingGroupBackup";

        /// <summary>
        /// 楼宇备份表
        /// buildingBackup
        /// </summary>
        public static readonly string BuildingBackup = "buildingBackup";

        /// <summary>
        /// 房间备份表
        /// roomBackup
        /// </summary>
        public static readonly string RoomBackup = "roomBackup";
        #endregion //Field
    }
}
