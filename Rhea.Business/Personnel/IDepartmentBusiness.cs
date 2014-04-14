﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Personnel;
using Rhea.Model;
using Rhea.Model.Personnel;

namespace Rhea.Business.Personnel
{
    /// <summary>
    /// 部门业务接口
    /// </summary>
    public interface IDepartmentBusiness
    {
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        List<Department> GetList();

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        Department Get(int id);

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="addition">附加数据</param>
        /// <returns></returns>
        Department Get(int id, DepartmentAdditionType addition);

        /// <summary>
        /// 得到部门名称
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        string GetName(int id);

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="data">部门数据</param>
        /// <returns>部门ID,0:添加失败</returns>
        int Create(Department data);

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="data">部门数据</param>
        /// <returns></returns>
        bool Edit(Department data);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// 编辑规模数据
        /// </summary>
        /// <param name="data">部门数据</param>
        /// <returns></returns>
        bool EditScale(Department data);

        /// <summary>
        /// 编辑科研数据
        /// </summary>
        /// <param name="data">部门数据</param>
        /// <returns></returns>
        bool EditResearch(Department data);

        /// <summary>
        /// 编辑特殊面积数据
        /// </summary>
        /// <param name="data">部门数据</param>
        /// <returns></returns>
        bool EditSpecialArea(Department data);

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="log">日志对象</param>
        /// <returns></returns>
        bool Log(int id, Log log);

        /// <summary>
        /// 备份部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        bool Backup(int id);

        /// <summary>
        /// 归档部门
        /// </summary>
        /// <param name="log">相关日志</param>
        /// <returns></returns>
        bool Archive(Log log);

        /// <summary>
        /// 得到归档部门数据
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="logId">日志ID</param>
        /// <returns></returns>
        Department GetArchive(int id, string logId);
    }
}