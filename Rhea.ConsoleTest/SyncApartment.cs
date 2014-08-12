using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data;
using Rhea.Data.Apartment;
using Rhea.Data.Mongo.Apartment;
using Rhea.Model;
using Rhea.Model.Apartment;
using Rhea.Common;

namespace Rhea.ConsoleTest
{
    public class SyncApartment
    {
        private Inhabitant AddInhabitant(DataRow row)
        {
            Inhabitant inhabitant = new Inhabitant();
            inhabitant.JobNumber = row["工号"].ToString();
            inhabitant.Name = row["姓名"].ToString();
            inhabitant.Type = 1;
            inhabitant.DepartmentName = row["部门"].ToString();
            inhabitant.Telephone = row["手机号码"].ToString();
            inhabitant.IdentityCard = row["身份证号码"].ToString();
            inhabitant.Status = 0;

            MongoInhabitantRepository repository = new MongoInhabitantRepository();
            ErrorCode result = repository.Create(inhabitant);

            Console.WriteLine("添加住户:{0}, {1}", inhabitant.Name, result.DisplayName());

            return inhabitant;
        }

        private ResideRecord AddResideRecord(DataRow row, Inhabitant inhabitant)
        {
            ResideRecord data = new ResideRecord();
            if (inhabitant != null)
                data.InhabitantId = inhabitant._id;
            data.RoomId = Convert.ToInt32(row["房间ID"]);
            data.InhabitantName = row["姓名"].ToString();
            data.InhabitantDepartment = row["部门"].ToString();
            data.ResideType = Convert.ToInt32(row["居住状态"]);

            if (row["房租"].ToString() == string.Empty)
                data.Rent = 0;
            else
                data.Rent = Convert.ToDecimal(row["房租"]);

            if (row["入住时间"].ToString() != string.Empty)
                data.EnterDate = Convert.ToDateTime(row["入住时间"]);

            if (row["到期时间"].ToString() != string.Empty)
                data.ExpireDate = Convert.ToDateTime(row["到期时间"]);

            data.TermLimit = row["年限"].ToString();
            data.LiHuStatus = row["蠡湖家园入住情况"].ToString();
            data.Remark = row["备注"].ToString();

            MongoResideRecordRepository repository = new MongoResideRecordRepository();
            ErrorCode result = repository.Create(data);

            Console.WriteLine("添加居住记录:{0}, {1},  {2}", data.RoomId, data.InhabitantName, result.DisplayName());

            return data;
        }

        private void GetData()
        {
            SqliteRepository repository = new SqliteRepository(@"E:\Test\rheaimport.sqlite");

            string sql = "SELECT * FROM Apartment";
            DataTable dt = repository.ExecuteQuery(sql);

            foreach(DataRow row in dt.Rows)
            {
                int status = Convert.ToInt32(row["居住状态"]);

                switch (status)
                {
                    case 0:
                        continue;
                    case 1:
                        
                        break;
                }

            }
        }
    }
}
