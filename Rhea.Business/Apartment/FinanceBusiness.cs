using Rhea.Data;
using Rhea.Data.Apartment;
using Rhea.Data.Estate;
using Rhea.Data.Mongo.Apartment;
using Rhea.Model;
using Rhea.Model.Apartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhea.Business.Apartment
{
    /// <summary>
    /// 收费业务类
    /// </summary>
    public class FinanceBusiness
    {
        #region Field
        /// <summary>
        /// 房间Repository
        /// </summary>
        private IRoomRepository roomRepository;

        /// <summary>
        /// 居住记录Repository
        /// </summary>
        IResideRecordRepository recordRepository;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 收费业务类
        /// </summary>
        public FinanceBusiness()
        {
            this.recordRepository = new MongoResideRecordRepository();
        }
        #endregion //Constructor

        #region Method
        public IEnumerable<RentRecord> Process(DateTime start, DateTime end)
        {
            List<RentRecord> list = new List<RentRecord>();
            DictionaryBusiness business = new DictionaryBusiness();
            var types = business.GetPairProperty("InhabitantType");

            ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
            var data = recordBusiness.GetByResideType(ResideType.Normal).Where(r => r.LeaveDate == null || r.LeaveDate >= start);

            foreach (var item in data)
            {
                RentRecord m = new RentRecord();

                var inhabitant = item.GetInhabitant();
                if (inhabitant.Type != 1 && inhabitant.Type != 7)
                    continue;

                m.ResideRecordId = item._id;
                m.InhabitantId = item.InhabitantId;
                m.InhabitantName = item.InhabitantName;
                m.InhabitantNumber = inhabitant.JobNumber;
                m.InhabitantDepartment = item.InhabitantDepartment;
                m.InhabitantType = types[inhabitant.Type];

                m.RoomNumber = item.GetApartmentRoom().Number;
                m.EnterDate = item.EnterDate;
                m.LeaveDate = item.LeaveDate;
                m.CurrentRent = item.Rent;
                m.Status = item.Status;

                list.Add(m);
            }

            return list;
        }
        #endregion //Method
    }
}
