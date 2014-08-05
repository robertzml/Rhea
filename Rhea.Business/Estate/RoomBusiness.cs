using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data.Estate;
using Rhea.Data.Mongo.Estate;
using Rhea.Model;
using Rhea.Model.Estate;

namespace Rhea.Business.Estate
{
    /// <summary>
    /// 房间业务类
    /// </summary>
    public class RoomBusiness
    {
        #region Field
        /// <summary>
        /// 房间Repository
        /// </summary>
        private IRoomRepository roomRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 房间业务类
        /// </summary>
        public RoomBusiness()
        {
            this.roomRepository = new MongoRoomRepository();
        }
        #endregion //Constructor
    }
}
