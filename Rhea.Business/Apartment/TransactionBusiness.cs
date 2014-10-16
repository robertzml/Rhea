﻿using System;
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
        /// <param name="data">退房办理业务记录对象</param>
        /// <returns></returns>
        private ErrorCode CreateCheckOutTransaction(CheckOutTransaction data)
        {
            ITransactionRepository repository = new MongoCheckOutTransactionRepository();
            return repository.Create(data);
        }

        /// <summary>
        /// 添加延期办理业务记录
        /// </summary>
        /// <param name="data">延期办理业务记录对象</param>
        /// <returns></returns>
        private ErrorCode CreateExtendTransaction(ExtendTransaction data)
        {
            ITransactionRepository repository = new MongoExtendTransactionRepository();
            return repository.Create(data);
        }

        /// <summary>
        /// 添加特殊换房办理业务记录
        /// </summary>
        /// <param name="data">特殊换房办理业务记录对象</param>
        /// <returns></returns>
        private ErrorCode CreateSpecialExchangeTransaction(SpecialExchangeTransaction data)
        {
            ITransactionRepository repository = new MongoSpecialExchangeTransaction();
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
                    Content = string.Format("青教普通入住业务办理, 住户ID:{0}, 姓名:{1}, 部门:{2}, 入住房间ID:{3}, 房间名称:{4}, 入住时间:{5}, 到期时间:{6}, 居住类型:正常居住, 记录ID:{7}, 业务ID:{8}。",
                        inhabitant._id, inhabitant.Name, inhabitant.DepartmentName, room.RoomId, room.Name, record.EnterDate, record.ExpireDate, record._id, transaction._id),
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
                    Content = string.Format("退房业务办理, 住户ID:{0}, 姓名:{1}, 部门:{2}, 入住房间ID:{3}, 房间名称:{4}, 入住时间:{5}, 退房时间:{6}, 记录ID:{7}, 业务ID:{8}。",
                        inhabitant._id, inhabitant.Name, inhabitant.DepartmentName, room.RoomId, room.Name, record.EnterDate, record.LeaveDate, record._id, transaction._id),
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
        /// 延期办理
        /// </summary>
        /// <param name="inhabitantId">住户ID</param>
        /// <param name="record">居住记录对象</param>
        /// <param name="roomId">房间ID</param>
        /// <param name="user">操作用户</param>
        /// <returns></returns>
        public ErrorCode Extend(string inhabitantId, ResideRecord record, int roomId, User user)
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

                //检查原记录
                ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
                var records = recordBusiness.GetByInhabitant(inhabitantId); //住户所有居住记录
                if (records == null || records.Count() == 0)
                    return ErrorCode.ResideRecordNotExist;
                ResideRecord lastRecord = records.SingleOrDefault(r => r.RoomId == roomId &&
                    (r.Status == (int)EntityStatus.Normal || r.Status == (int)EntityStatus.ExtendTime || r.Status == (int)EntityStatus.OverTime));
                if (lastRecord == null)
                    return ErrorCode.ResideRecordNotExist;


                //更新原居住记录
                lastRecord.LeaveDate = record.EnterDate;
                lastRecord.Status = (int)EntityStatus.ExtendOut;
                result = recordBusiness.Update(lastRecord);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "居住记录更新出错：" + result.DisplayName();
                    return result;
                }

                //更新住户状态
                inhabitant.Status = (int)GetInhabitantStatus(inhabitant._id);
                if (inhabitant.Status != (int)EntityStatus.InhabitantExpire)
                    inhabitant.Status = (int)EntityStatus.InhabitantExtend;
                result = inhabitantBusiness.Update(inhabitant);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "住户状态更新出错：" + result.DisplayName();
                    return result;
                }

                //添加新居住记录
                record.InhabitantId = inhabitant._id;
                record.InhabitantName = inhabitant.Name;
                record.InhabitantDepartment = inhabitant.DepartmentName;
                record.RoomId = roomId;
                record.RegisterTime = now;
                result = recordBusiness.Create(record);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "居住记录添加出错：" + result.DisplayName();
                    return result;
                }

                //添加业务记录
                ExtendTransaction transaction = new ExtendTransaction();
                transaction.Type = (int)LogType.ApartmentExtend;
                transaction.Time = now;
                transaction.UserId = user._id;
                transaction.UserName = user.Name;
                transaction.RoomId = room.RoomId;
                transaction.RoomName = room.Name;
                transaction.InhabitantId = inhabitant._id;
                transaction.InhabitantName = inhabitant.Name;
                transaction.ResideRecordId = record._id;
                transaction.OldResideRecordId = lastRecord._id;
                transaction.Remark = "";
                transaction.Status = 0;
                result = CreateExtendTransaction(transaction);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "添加业务记录失败:" + result.DisplayName();
                    return result;
                }

                //生成日志
                LogBusiness logBusiness = new LogBusiness();
                Log log = new Log
                {
                    Title = "延期业务办理",
                    Time = now,
                    Type = (int)LogType.ApartmentExtend,
                    Content = string.Format("青教延期业务办理, 住户ID:{0}, 姓名:{1}, 部门:{2}, 入住房间ID:{3}, 房间名称:{4}, 入住时间:{5}, 到期时间:{6}, 居住类型:延期居住, 记录ID:{7}, 业务ID:{8}, 附件:{9}。",
                        inhabitant._id, inhabitant.Name, inhabitant.DepartmentName, room.RoomId, room.Name, record.EnterDate, record.ExpireDate, record._id, transaction._id, string.Join(",", record.Files)),
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
                recordBusiness.LogItem(lastRecord._id, log);
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
        /// 特殊换房业务办理
        /// </summary>
        /// <param name="inhabitantId">住户ID</param>
        /// <param name="oldRoomId">原房间ID</param>
        /// <param name="newRoomId">新房间ID</param>
        /// <param name="remark">备注</param>
        /// <param name="user">操作用户</param>
        /// <remarks>
        /// 该业务直接将入住记录从原房间绑定到新房间上，记录信息不做更改。仅限因特殊原因入住前换房。
        /// </remarks>
        /// <returns></returns>
        public ErrorCode SpecialExchange(string inhabitantId, int oldRoomId, int newRoomId, string remark, User user)
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

                //检查新房间
                ApartmentRoomBusiness roomBusiness = new ApartmentRoomBusiness();
                ApartmentRoom room = roomBusiness.Get(newRoomId);
                if (room == null)
                    return ErrorCode.ObjectNotFound;
                if (room.ResideType != (int)ResideType.Available)
                    return ErrorCode.RoomNotAvailable;

                //备份新房间
                result = roomBusiness.Backup(room._id);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "备份新房间失败: " + result.DisplayName();
                    return result;
                }

                //检查原房间
                ApartmentRoom oldRoom = roomBusiness.Get(oldRoomId);
                if (oldRoom == null)
                    return ErrorCode.ObjectNotFound;
                if (oldRoom.ResideType != (int)ResideType.Normal)
                    return ErrorCode.RoomAvailable;

                //备份原房间
                result = roomBusiness.Backup(oldRoom._id);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "备份原房间失败: " + result.DisplayName();
                    return result;
                }

                //检查居住记录
                ResideRecordBusiness recordBusiness = new ResideRecordBusiness();
                var records = recordBusiness.GetByInhabitant(inhabitantId); //住户所有居住记录
                if (records == null || records.Count() == 0)
                    return ErrorCode.ResideRecordNotExist;
                ResideRecord record = records.SingleOrDefault(r => r.RoomId == oldRoomId &&
                    (r.Status == (int)EntityStatus.Normal || r.Status == (int)EntityStatus.ExtendTime || r.Status == (int)EntityStatus.OverTime));
                if (record == null)
                    return ErrorCode.ResideRecordNotExist;

                //更新原房间状态              
                oldRoom.ResideType = (int)ResideType.Available;
                result = roomBusiness.Update(oldRoom);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "原房间状态更新出错：" + result.DisplayName();
                    return result;
                }

                //更新新房间状态
                room.ResideType = (int)ResideType.Normal;
                result = roomBusiness.Update(room);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "新房间状态更新出错：" + result.DisplayName();
                    return result;
                }

                //修改居住记录
                record.RoomId = room.RoomId;
                result = recordBusiness.Update(record);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "修改居住记录出错：" + result.DisplayName();
                    return result;
                }

                //添加业务记录
                SpecialExchangeTransaction transaction = new SpecialExchangeTransaction();
                transaction.Type = (int)LogType.ApartmentSpecialExchange;
                transaction.Time = now;
                transaction.UserId = user._id;
                transaction.UserName = user.Name;
                transaction.OldRoomId = oldRoom.RoomId;
                transaction.OldRoomName = oldRoom.Name;
                transaction.NewRoomId = room.RoomId;
                transaction.NewRoomName = room.Name;
                transaction.InhabitantId = inhabitant._id;
                transaction.InhabitantName = inhabitant.Name;
                transaction.ResideRecordId = record._id;
                transaction.Remark = remark;
                transaction.Status = 0;
                result = CreateSpecialExchangeTransaction(transaction);
                if (result != ErrorCode.Success)
                {
                    this.errorMessage = "添加业务记录失败:" + result.DisplayName();
                    return result;
                }

                //生成日志
                LogBusiness logBusiness = new LogBusiness();
                Log log = new Log
                {
                    Title = "特殊换房业务办理",
                    Time = now,
                    Type = (int)LogType.ApartmentSpecialExchange,
                    Content = string.Format("青教特殊换房业务办理, 住户ID:{0}, 姓名:{1}, 部门:{2}, 原房间ID:{3}, 原房间名称:{4}, 新房间ID:{5}, 新房间名称:{6}, 入住时间:{7}, 到期时间:{8}, 记录ID:{9}, 业务ID:{10}。",
                        inhabitant._id, inhabitant.Name, inhabitant.DepartmentName, oldRoom.RoomId, oldRoom.Name, room.RoomId, room.Name, record.EnterDate, record.ExpireDate, record._id, transaction._id),
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
                roomBusiness.LogItem(oldRoom._id, log);
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
        /// 获取所有业务记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApartmentTransaction> GetTransaction()
        {
            ITransactionRepository repository = new MongoTransactionRepository();
            return repository.Get().OrderByDescending(r => r.Time);
        }

        /// <summary>
        /// 获取业务记录
        /// </summary>
        /// <param name="id">业务记录ID</param>
        /// <returns></returns>
        public ApartmentTransaction GetTransaction(string id)
        {
            ITransactionRepository repository = new MongoTransactionRepository();
            return repository.Get(id);
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
        /// 获取入住办理业务记录
        /// </summary>
        /// <param name="id">业务记录ID</param>
        /// <returns></returns>
        public CheckInTransaction GetCheckInTransaction(string id)
        {
            ITransactionRepository repository = new MongoCheckInTransactionRepository();
            var data = (CheckInTransaction)repository.Get(id);
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

        /// <summary>
        /// 获取退房办理业务记录
        /// </summary>
        /// <param name="id">业务记录ID</param>
        /// <returns></returns>
        public CheckOutTransaction GetCheckOutTransaction(string id)
        {
            ITransactionRepository repository = new MongoCheckOutTransactionRepository();
            var data = (CheckOutTransaction)repository.Get(id);
            return data;
        }

        /// <summary>
        /// 获取所有延期办理业务记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ExtendTransaction> GetExtendTransaction()
        {
            ITransactionRepository repository = new MongoExtendTransactionRepository();
            var data = repository.Get().Cast<ExtendTransaction>().OrderByDescending(r => r.Time);
            return data;
        }

        /// <summary>
        /// 获取延期办理业务记录
        /// </summary>
        /// <param name="id">业务记录ID</param>
        /// <returns></returns>
        public ExtendTransaction GetExtendTransaction(string id)
        {
            ITransactionRepository repository = new MongoExtendTransactionRepository();
            var data = (ExtendTransaction)repository.Get(id);
            return data;
        }

        /// <summary>
        /// 获取所有特殊换房业务记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SpecialExchangeTransaction> GetSpecialExchangeTransaction()
        {
            ITransactionRepository repository = new MongoSpecialExchangeTransaction();
            var data = repository.Get().Cast<SpecialExchangeTransaction>().OrderByDescending(r => r.Time);
            return data;
        }

        /// <summary>
        /// 获取特殊换房业务记录
        /// </summary>
        /// <param name="id">业务记录ID</param>
        /// <returns></returns>
        public SpecialExchangeTransaction GetSpecialExchangeTransaction(string id)
        {
            ITransactionRepository repository = new MongoSpecialExchangeTransaction();
            var data = (SpecialExchangeTransaction)repository.Get(id);
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