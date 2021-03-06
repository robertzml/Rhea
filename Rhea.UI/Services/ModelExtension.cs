﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Rhea.Business.Account;
using Rhea.Model.Account;

namespace Rhea.UI.Services
{
    public static class ModelExtension
    {
        /// <summary>
        /// 自定义角色判断
        /// </summary>
        /// <param name="user">当前用户信息</param>
        /// <param name="role">角色</param>
        /// <returns></returns>
        public static bool IsInRole2(this IPrincipal user, string role)
        {
            if (user != null && user.Identity.IsAuthenticated)
            {
                FormsIdentity fi = (FormsIdentity)user.Identity;
                string[] roles = fi.Ticket.UserData.Split(',');
                if (roles.Contains(role))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// 得到用户级别
        /// </summary>
        /// <param name="user">登录用户</param>
        /// <returns></returns>
        public static int GetRank(this IPrincipal user)
        {
            if (user != null && user.Identity.IsAuthenticated)
            {
                FormsIdentity fi = (FormsIdentity)user.Identity;
                string userRole = fi.Ticket.UserData;

                UserBusiness business = new UserBusiness();
                var group = business.GetUserGroup(userRole);
                if (group == null)
                    return 0;
                else
                    return group.Rank;
            }
            else
                return 0;
        }

        /// <summary>
        /// 得到用户组名称
        /// </summary>
        /// <param name="user">登录用户</param>
        /// <returns></returns>
        public static string GetRole(this IPrincipal user)
        {
            if (user != null && user.Identity.IsAuthenticated)
            {
                FormsIdentity fi = (FormsIdentity)user.Identity;
                string userRole = fi.Ticket.UserData;

                UserBusiness business = new UserBusiness();
                var group = business.GetUserGroup(userRole);
                if (group == null)
                    return "";
                else
                    return group.Name;
            }
            else
                return "";
        }

        /// <summary>
        /// 得到用户组名称
        /// </summary>
        /// <param name="user">登录用户</param>
        /// <returns></returns>
        public static bool HasPrivilege(this IPrincipal user, string require)
        {
            if (user != null && user.Identity.IsAuthenticated)
            {
                int rootId = 100001;

                UserBusiness userBusiness = new UserBusiness();
                User u = userBusiness.GetByUserName(user.Identity.Name);
                UserGroup group = userBusiness.GetUserGroup(u.UserGroupId);

                if (group.UserGroupId == rootId)
                    return true;

                if (group.UserGroupPrivilege.Any(r => r.Name == require))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}