﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Data.Server;
using Rhea.Model.Personnel;

namespace Rhea.Business.Personnel
{
    /// <summary>
    /// 部门业务类
    /// </summary>
    public class MongoDepartmentBusiness : IDepartmentBusiness
    {
        #region Field
        /// <summary>
        /// 数据库连接
        /// </summary>
        private RheaMongoContext context = new RheaMongoContext(RheaServer.PersonnelDatabase);
        #endregion //Field

        #region Function
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private Department ModelBind(BsonDocument doc)
        {
            Department department = new Department();
            department.Id = doc["id"].AsInt32;
            department.Name = doc["name"].AsString;
            department.Type = doc["type"].AsInt32;
            department.Description = doc.GetValue("description", "").AsString;
            department.ImageUrl = doc.GetValue("imageUrl", "").AsString;
            department.LogoUrl = doc.GetValue("logoUrl", "").AsString;
            department.ShortName = doc.GetValue("shortName", "").AsString;
            department.Status = doc.GetValue("status", 0).AsInt32;

            return department;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        public List<Department> GetList()
        {
            List<Department> departments = new List<Department>();
            List<BsonDocument> doc = this.context.FindAll(PersonnelCollection.Department);

            foreach (var d in doc)
            {
                if (d.GetValue("status", 0).AsInt32 == 1)
                    continue;
                Department department = ModelBind(d);
                departments.Add(department);
            }

            departments = departments.OrderBy(r => r.Id).ToList();
            return departments;
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public Department Get(int id)
        {
            BsonDocument doc = this.context.FindOne(PersonnelCollection.Department, "id", id);
            if (doc != null)
            {
                Department department = ModelBind(doc);
                if (department.Status == 1)
                    return null;

                return department;
            }
            else
                return null;
        }

        /// <summary>
        /// 得到部门名称
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public string GetName(int id)
        {
            BsonDocument doc = this.context.FindOne(PersonnelCollection.Department, "id", id);

            if (doc != null)
                return doc["name"].AsString;
            else
                return string.Empty;
        }

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="data">部门数据</param>
        /// <returns>部门ID,0:添加失败</returns>
        public int Create(Department data)
        {
            bool dup = this.context.CheckDuplicateId(PersonnelCollection.Department, data.Id);
            if (dup)
                return 0;

            BsonDocument doc = new BsonDocument
            {
                { "id", data.Id },                
                { "name" , data.Name },  
                { "shortName", data.ShortName ?? "" },
                { "imageUrl", data.ImageUrl ?? "" },
                { "logoUrl", data.LogoUrl ?? "" },
                { "type", data.Type },           
                { "description", data.Description ?? "" },
                { "status", 0 }
            };

            WriteConcernResult result = this.context.Insert(PersonnelCollection.Department, doc);

            if (result.HasLastErrorMessage)
                return 0;
            else
                return data.Id;
        }

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="data">部门数据</param>
        /// <returns></returns>
        public bool Edit(Department data)
        {
            var query = Query.EQ("id", data.Id);
            var update = Update.Set("name", data.Name)
                .Set("shortName", data.ShortName ?? "")
                .Set("type", data.Type)
                .Set("description", data.Description ?? "")
                .Set("imageUrl", data.ImageUrl ?? "")
                .Set("logoUrl", data.LogoUrl ?? "");

            WriteConcernResult result = this.context.Update(PersonnelCollection.Department, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var query = Query.EQ("id", id);
            var update = Update.Set("status", 1);

            WriteConcernResult result = this.context.Update(PersonnelCollection.Department, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }
        #endregion //Method
    }
}
