using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhea.Data.Entities
{
    /// <summary>
    /// 房间模型
    /// </summary>
    public class Room
    {
        #region Constructor
        public Room()
        {
            this.Function = new RoomFunction();
            this.Building = new RoomBuilding();
            this.Department = new RoomDepartment();
        }
        #endregion //Constructor

        #region Property
        /// <summary>
        /// 房间ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public int Floor { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        public double UsableArea { get; set; }

        /// <summary>
        /// 临时ID
        /// </summary>
        public string TempId { get; set; }

        /// <summary>
        /// 房间功能
        /// </summary>
        public RoomFunction Function { get; set; }

        /// <summary>
        /// 所属楼宇
        /// </summary>
        public RoomBuilding Building { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public RoomDepartment Department { get; set; }
        #endregion //Property

        #region Inner Class
        /// <summary>
        /// 房间功能
        /// </summary>
        public class RoomFunction
        {
            public int FirstCode { get; set; }

            public int SecondCode { get; set; }

            public string Property { get; set; }
        }

        public class RoomBuilding
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class RoomDepartment
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
        #endregion Inner Class
    }
}
