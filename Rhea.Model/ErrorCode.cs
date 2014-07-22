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
        /// ID重复
        /// </summary>
        [Display(Name = "ID重复")]
        DuplicateId = 30,

        /// <summary>
        /// 名称重复
        /// </summary>
        [Display(Name = "名称重复")]
        DuplicateName = 31
    }
}
