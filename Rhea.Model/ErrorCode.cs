using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Rhea.Model
{
    /// <summary>
    /// 错误代码
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Display(Name = "成功")]
        Success = 0,

        /// <summary>
        /// 错误
        /// </summary>
        [Display(Name = "错误")]
        Error = 1,

        /// <summary>
        /// 异常
        /// </summary>
        [Display(Name = "异常")]
        Exception = 2,

        /// <summary>
        /// 未实现程序
        /// </summary>
        [Display(Name = "未实现程序")]
        NotImplement = 3,

        /// <summary>
        /// 对象已删除
        /// </summary>
        [Display(Name = "对象已删除")]
        ObjectDeleted = 4,

        /// <summary>
        /// 对象未找到
        /// </summary>
        [Display(Name = "对象未找到")]
        ObjectNotFound = 5,

        /// <summary>
        /// 数据库写入失败
        /// </summary>
        [Display(Name = "数据库写入失败")]
        DatabaseWriteError = 6,

        /// <summary>
        /// 用户ID重复
        /// </summary>
        [Display(Name = "用户ID重复")]
        DuplicateUserId = 10,

        /// <summary>
        /// 用户名称重复
        /// </summary>
        [Display(Name = "用户名称重复")]
        DuplicateUserName = 11,

        /// <summary>
        /// 密码错误
        /// </summary>
        [Display(Name = "密码错误")]
        WrongPassword = 12,

        /// <summary>
        /// 用户不存在
        /// </summary>
        [Display(Name = "用户不存在")]
        UserNotExist = 13,

        /// <summary>
        /// 用户已禁用
        /// </summary>
        [Display(Name = "用户已禁用")]
        UserDisabled = 14,

        /// <summary>
        /// ID重复
        /// </summary>
        [Display(Name = "ID重复")]
        DuplicateId = 30,

        /// <summary>
        /// 名称重复
        /// </summary>
        [Display(Name = "名称重复")]
        DuplicateName = 31,

        /// <summary>
        /// 楼层已存在
        /// </summary>
        [Display(Name = "楼层已存在")]
        FloorExist = 32,

        /// <summary>
        /// 房间被占用
        /// </summary>
        [Display(Name = "房间被占用")]
        RoomNotAvailable = 50,

        /// <summary>
        /// 房间可用
        /// </summary>
        [Display(Name = "房间可用")]
        RoomAvailable = 51,

        /// <summary>
        /// 住户已搬出
        /// </summary>
        [Display(Name = "住户已搬出")]
        InhabitantMoveOut = 55,

        /// <summary>
        /// 住户不存在
        /// </summary>
        [Display(Name = "住户不存在")]
        InhabitantNotExist = 56,

        /// <summary>
        /// 住户已超期
        /// </summary>
        [Display(Name = "住户已超期")]
        InhabitantExpired = 57,

        /// <summary>
        /// 居住记录不存在
        /// </summary>
        [Display(Name = "居住记录不存在")]
        ResideRecordNotExist = 60
    }
}
