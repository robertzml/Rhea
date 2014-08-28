using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Common;
using Rhea.Model;
using Rhea.Model.Account;
using Rhea.Model.Apartment;
using Rhea.Data.Apartment;
using Rhea.Data.Mongo.Apartment;

namespace Rhea.Business.Apartment
{
    /// <summary>
    /// 青教业务类
    /// </summary>
    public class TransactionBusiness
    {
        #region Field
        /// <summary>
        /// 错误消息
        /// </summary>
        private string errorMessage = "";
        #endregion //Field

        #region Function
        /// <summary>
        /// 检测住户状态
        /// </summary>
        /// <param name="inhabitantId">住户ID</param>
        /// <remarks>
        /// 根据住户的居住记录，选取特殊状态为住户当前状态
        /// </remarks>
        /// <returns></returns>
        private EntityStatus GetInhabitantStatus(string inhabitantId)
        {
            ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
            var records = recordBusiness.GetByInhabitant(inhabitantId); //住户所有居住记录

            if (records.Any(r => r.Status == (int)EntityStatus.OverTime))
                return EntityStatus.InhabitantExpire;
            else if (records.Any(r => r.Status == (int)EntityStatus.ExtendTime))
                return EntityStatus.InhabitantExtend;
            else if (records.Any(r => r.Status == (int)EntityStatus.Normal))
                return EntityStatus.Normal;
            else
                return EntityStatus.InhabitantMoveOut;
        }

        /// <summary>
        /// 添加入住办理业务记录
        /// </summary>
        /// <param name="data">入住办理业务记录对象</param>
        /// <returns></returns>
        private ErrorCode CreateCheckInTransaction(CheckInTransaction data)
        {
            ITransactionRepository repository = new MongoCheckInTransactionRepository();
            return repository.Create(data);
        }

        /// <summary>
        /// 添加退房办理业务记录
        /// </summary>
        /// <param name="data">退房办理业务记录</param>
        /// <returns></returns>
        private ErrorCode CreateCheckOutTransaction(CheckOutTransaction data)
        {
            ITransactionRepository repository = new MongoCheckOutTransactionRepository();
            return repository.Create(data);
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 入住办理业务流程
        /// </summary>
        /// <param name="inhabitant">住户信息</param>
        /// <param name="record">记录信息</param>
        /// <param name="roomId">房间ID</param>
        /// <param name="user">操作用户</param>
        /// <returns></returns>
        public ErrorCode CheckIn(Inhabitant inhabitant, ResideRecord record, int roomId, User user)
        {
            try
            {
                ErrorCode result;
                DateTime now = DateTime.Now;

                //检查房间状态
                ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
                var room = roomBusiness.Get(roomId);
                if (room == null)
                    return ErrorCode.ObjectNotFound;
                if (room.ResideType != (int)ResideType.Available)
                    return ErrorCode.RoomNotAvailable;

                //备份房间
                result = roomBusiness.Backup(room._id);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "备份房间失败: " + result.DisplayName();
                    return result;
                }

                //更新房间状态
                room.ResideType = (int)ResideType.Normal;
                result = roomBusiness.Update(room);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "房间状态更新出错：" + result.DisplayName();
                    return result;
                }

                //添加住户                
                InhabitantBusiness inhabitantBusiness = new InhabitantBusiness();
                if (string.IsNullOrEmpty(inhabitant._id))
                {
                    inhabitant.Status = 0;
                    result = inhabitantBusiness.Create(inhabitant);
                }
                else
                {
                    inhabitant.Status = (int)GetInhabitantStatus(inhabitant._id);
                    result = inhabitantBusiness.Update(inhabitant);
                }
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "用户添加出错：" + result.DisplayName();
                    return result;
                }

                //添加居住记录
                ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
                record.InhabitantId = inhabitant._id;
                record.InhabitantName = inhabitant.Name;
                record.InhabitantDepartment = inhabitant.DepartmentName;
                record.RegisterTime = now;
                result = recordBusiness.Create(record);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "居住记录添加出错：" + result.DisplayName();
                    return result;
                }

                //添加业务记录
                CheckInTransaction transaction = new CheckInTransaction();
                transaction.Type = (int)LogType.ApartmentCheckIn;
                transaction.Time = now;
                transaction.UserId = user._id;
                transaction.UserName = user.Name;
                transaction.RoomId = room.RoomId;
                transaction.RoomName = room.Name;
                transaction.InhabitantId = inhabitant._id;
                transaction.InhabitantName = inhabitant.Name;
                transaction.ResideRecordId = record._id;
                transaction.Remark = "";
                transaction.Status = 0;
                result = CreateCheckInTransaction(transaction);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "添加业务记录失败:" + result.DisplayName();
                    return result;
                }

                //生成日志
                LogBusiness logBusiness = new LogBusiness();
                Log log = new Log
                {
                    Title = "入住业务办理",
                    Time = now,
                    Type = (int)LogType.ApartmentCheckIn,
                    Content = string.Format("青教普通入住业务办理, 住户ID:{0}, 姓名:{1}, 部门:{2}, 入住房间ID:{3}, 房间名称:{4}, 入住时间:{5}, 到期时间:{6}, 居住类型:正常居住, 记录ID:{7}。",
                        inhabitant._id, inhabitant.Name, inhabitant.DepartmentName, room.RoomId, room.Name, record.EnterDate, record.ExpireDate, record._id),
                    UserId = user._id,
                    UserName = user.Name,
                    Tag = record._id
                };
                result = logBusiness.Create(log);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "记录日志失败:" + result.DisplayName();
                    return result;
                }

                //更新日志
                inhabitantBusiness.LogItem(inhabitant._id, log);
                recordBusiness.LogItem(record._id, log);
                roomBusiness.LogItem(room._id, log);
            }
            catch (Exception e)
            {
                this.errorMessage = e.Message;
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 退房办理业务流程
        /// </summary>
        /// <param name="inhabitantId">住户ID</param>
        /// <param name="roomId">房间ID</param>
        /// <param name="leaveDate">退房时间</param>
        /// <param name="remark">备注</param>
        /// <param name="user">操作用户</param>
        /// <returns></returns>
        public ErrorCode CheckOut(string inhabitantId, int roomId, DateTime leaveDate, string remark, User user)
        {
            try
            {
                ErrorCode result;
                DateTime now = DateTime.Now;

                //检查用户
                InhabitantBusiness inhabitantBusiness = new InhabitantBusiness();
                Inhabitant inhabitant = inhabitantBusiness.Get(inhabitantId);
                if (inhabitant == null)
                    return ErrorCode.ObjectNotFound;
                if (inhabitant.Status == (int)EntityStatus.InhabitantMoveOut)
                    return ErrorCode.InhabitantMoveOut;

                //检查房间
                ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
                ApartmentRoom room = roomBusiness.Get(roomId);
                if (room == null)
                    return ErrorCode.ObjectNotFound;
                if (room.ResideType == (int)ResideType.Available)
                    return ErrorCode.RoomAvailable;

                //检查记录
                ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
                var records = recordBusiness.GetByInhabitant(inhabitantId); //住户所有居住记录
                if (records == null || records.Count() == 0)
                    return ErrorCode.ResideRecordNotExist;
                ResideRecord record = records.SingleOrDefault(r => r.RoomId == roomId &&
                    (r.Status == (int)EntityStatus.Normal || r.Status == (int)EntityStatus.ExtendTime || r.Status == (int)EntityStatus.OverTime));
                if (record == null)
                    return ErrorCode.ResideRecordNotExist;

                //备份房间
                result = roomBusiness.Backup(room._id);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "备份房间失败: " + result.DisplayName();
                    return result;
                }

                //更新房间状态
                room.ResideType = (int)ResideType.Available;
                result = roomBusiness.Update(room);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "房间状态更新出错：" + result.DisplayName();
                    return result;
                }

                //更新居住记录
                record.LeaveDate = leaveDate;
                record.Remark += remark;
                record.Status = (int)EntityStatus.MoveOut;
                result = recordBusiness.Update(record);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "居住记录更新出错：" + result.DisplayName();
                    return result;
                }

                //更新住户状态
                inhabitant.Status = (int)GetInhabitantStatus(inhabitant._id);
                result = inhabitantBusiness.Update(inhabitant);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "住户状态更新出错：" + result.DisplayName();
                    return result;
                }

                //添加业务记录
                CheckOutTransaction transaction = new CheckOutTransaction();
                transaction.Type = (int)LogType.ApartmentCheckOut;
                transaction.Time = now;
                transaction.UserId = user._id;
                transaction.UserName = user.Name;
                transaction.RoomId = room.RoomId;
                transaction.RoomName = room.Name;
                transaction.InhabitantId = inhabitant._id;
                transaction.InhabitantName = inhabitant.Name;
                transaction.ResideRecordId = record._id;
                transaction.Remark = "";
                transaction.Status = 0;
                result = CreateCheckOutTransaction(transaction);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "添加业务记录失败:" + result.DisplayName();
                    return result;
                }

                //生成日志
                LogBusiness logBusiness = new LogBusiness();
                Log log = new Log
                {
                    Title = "退房业务办理",
                    Time = now,
                    Type = (int)LogType.ApartmentCheckOut,
                    Content = string.Format("退房业务办理, 住户ID:{0}, 姓名:{1}, 部门:{2}, 入住房间ID:{3}, 房间名称:{4}, 入住时间:{5}, 退房时间:{6}, 记录ID:{7}。",
                        inhabitant._id, inhabitant.Name, inhabitant.DepartmentName, room.RoomId, room.Name, record.EnterDate, record.LeaveDate, record._id),
                    UserId = user._id,
                    UserName = user.Name,
                    Tag = record._id
                };
                result = logBusiness.Create(log);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "记录日志失败:" + result.DisplayName();
                    return result;
                }

                //更新日志
                inhabitantBusiness.LogItem(inhabitant._id, log);
                recordBusiness.LogItem(record._id, log);
                roomBusiness.LogItem(room._id, log);
            }
            catch (Exception e)
            {
                this.errorMessage = e.Message;
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 更新所有居住状态
        /// </summary>
        /// <remarks>
        /// 根据到期时间更新房间居住记录状态为超期，同时更新对应住户状态。
        /// 仅检查正常居住和延期居住的情况
        /// </remarks>
        /// <returns></returns>
        public ErrorCode UpdateStatus()
        {
            try
            {
                DateTime now = DateTime.Now;
                ErrorCode result;

                //获取相关居住记录
                InhabitantBusiness inhabitantBusiness = new InhabitantBusiness();
                ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
                var records = recordBusiness.Get().Where(r => r.Status == (int)EntityStatus.Normal || r.Status == (int)EntityStatus.ExtendTime);

                foreach (var record in records)
                {
                    if (record.ExpireDate == null)
                        continue;

                    DateTime expireDate = record.ExpireDate.Value;
                    if (expireDate >= now)
                        continue;

                    //更新居住记录
                    record.Status = (int)EntityStatus.OverTime;
                    result = recordBusiness.Update(record);
                    if (result != ErrorCode.Success)
                    {
                        this.errorMessage = result.DisplayName();
                        return result;
                    }

                    Inhabitant inhabitant = inhabitantBusiness.Get(record.InhabitantId);
                    if (inhabitant == null)
                    {
                        this.errorMessage = ErrorCode.InhabitantNotExist.DisplayName();
                        return ErrorCode.InhabitantNotExist;
                    }
                    inhabitant.Status = (int)EntityStatus.InhabitantExpire;
                    result = inhabitantBusiness.Update(inhabitant);
                    if (result != ErrorCode.Success)
                    {
                        this.errorMessage = result.DisplayName();
                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                this.errorMessage = e.Message;
                return ErrorCode.Exception;
            }

            return ErrorCode.Success;
        }

        /// <summary>
        /// 获取所有入住办理业务记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CheckInTransaction> GetCheckInTransaction()
        {
            ITransactionRepository repository = new MongoCheckInTransactionRepository();
            var data = repository.Get().Cast<CheckInTransaction>().OrderByDescending(r => r.Time);
            return data;
        }

        /// <summary>
        /// 获取所有退房办理业务记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CheckOutTransaction> GetCheckOutTransaction()
        {
            ITransactionRepository repository = new MongoCheckOutTransactionRepository();
            var data = repository.Get().Cast<CheckOutTransaction>().OrderByDescending(r => r.Time);
            return data;
        }
        #endregion //Method

        #region Property
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
        }
        #endregion //Property
    }
}
