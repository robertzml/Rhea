using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Rhea.Data.Personnel;
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

        /// <summary>
        /// 绑定学院规模数据
        /// </summary>
        /// <param name="doc">原文档</param>
        /// <param name="department">部门模型</param>
        private void BindCollegeScale(BsonDocument doc, ref Department department)
        {
            if (department.Type != (int)DepartmentType.Type1)
                return;

            department.BachelorCount = doc.GetValue("bachelorCount", 0).AsInt32;
            department.GraduateCount = doc.GetValue("graduateCount", 0).AsInt32;
            department.MasterOfEngineerCount = doc.GetValue("masterOfEngineerCount", 0).AsInt32;
            department.DoctorCount = doc.GetValue("doctorCount", 0).AsInt32;
            department.StaffCount = doc.GetValue("staffCount", 0).AsInt32;
            department.PartyLeaderCount = doc.GetValue("partyLeaderCount", 0).AsInt32;
            department.SectionChiefCount = doc.GetValue("sectionChiefCount", 0).AsInt32;
            department.ProfessorCount = doc.GetValue("professorCount", 0).AsInt32;
            department.AssociateProfessorCount = doc.GetValue("associateProfessorCount", 0).AsInt32;
            department.MediumTeacherCount = doc.GetValue("mediumTeacherCount", 0).AsInt32;
            department.AdvanceAssistantCount = doc.GetValue("advanceAssistantCount", 0).AsInt32;
            department.MediumAssistantCount = doc.GetValue("mediumAssistantCount", 0).AsInt32;
            department.SubjectType = doc.GetValue("subjectType", 1).AsInt32;
            department.FactorK1 = doc.GetValue("factorK1", 0.0).AsDouble;
            department.FactorK3 = doc.GetValue("factorK3", 0.0).AsDouble;

            return;
        }

        /// <summary>
        /// 绑定学院科研数据
        /// </summary>
        /// <param name="doc">原文档</param>
        /// <param name="department">部门模型</param>
        private void BindCollegeResearch(BsonDocument doc, ref Department department)
        {
            if (department.Type != (int)DepartmentType.Type1)
                return;

            department.LongitudinalFunds = doc.GetValue("longitudinalFunds", 0.0).AsDouble;
            department.TransverseFunds = doc.GetValue("transverseFunds", 0.0).AsDouble;
            department.CompanyFunds = doc.GetValue("companyFunds", 0.0).AsDouble;

            return;
        }

        /// <summary>
        /// 绑定学院特殊面积
        /// </summary>
        /// <param name="doc">原文档</param>
        /// <param name="department">部门模型</param>
        private void BindCollegeSpecialArea(BsonDocument doc, ref Department department)
        {
            if (department.Type != (int)DepartmentType.Type1)
                return;

            department.TalentArea = doc.GetValue("talentArea", 0.0).AsDouble;
            department.ResearchBonusArea = doc.GetValue("researchBonusArea", 0.0).AsDouble;
            department.ExperimentBonusArea = doc.GetValue("ExperimentBonusArea", 0.0).AsDouble;
            department.AdjustArea = doc.GetValue("adjustArea", 0.0).AsDouble;

            return;
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
        /// 获取部门
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <param name="addition">附加数据</param>
        /// <returns></returns>
        public Department Get(int id, DepartmentAdditionType addition)
        {
            BsonDocument doc = this.context.FindOne(PersonnelCollection.Department, "id", id);
            if (doc != null)
            {
                Department department = ModelBind(doc);
                if (department.Status == 1)
                    return null;

                if (department.Type == (int)DepartmentType.Type1)
                {
                    if ((addition & DepartmentAdditionType.ScaleData) != 0)
                        BindCollegeScale(doc, ref department);
                    if ((addition & DepartmentAdditionType.ResearchData) != 0)
                        BindCollegeResearch(doc, ref department);
                    if ((addition & DepartmentAdditionType.SpecialAreaData) != 0)
                        BindCollegeSpecialArea(doc, ref department);
                }

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

        /// <summary>
        /// 编辑规模数据
        /// </summary>
        /// <param name="data">部门数据</param>
        /// <returns></returns>
        public bool EditScale(Department data)
        {
            if (data.Type != (int)DepartmentType.Type1)
                return false;

            var query = Query.EQ("id", data.Id);
            var update = Update.Set("bachelorCount", data.BachelorCount)
                .Set("graduateCount", data.GraduateCount)
                .Set("masterOfEngineerCount", data.MasterOfEngineerCount)
                .Set("doctorCount", data.DoctorCount)
                .Set("staffCount", data.StaffCount)
                .Set("partyLeaderCount", data.PartyLeaderCount)
                .Set("sectionChiefCount", data.SectionChiefCount)
                .Set("professorCount", data.ProfessorCount)
                .Set("associateProfessorCount", data.AssociateProfessorCount)
                .Set("mediumTeacherCount", data.MediumTeacherCount)
                .Set("advanceAssistantCount", data.AdvanceAssistantCount)
                .Set("mediumAssistantCount", data.MediumAssistantCount)
                .Set("subjectType", data.SubjectType)
                .Set("factorK1", data.FactorK1)
                .Set("factorK3", data.FactorK3);

            WriteConcernResult result = this.context.Update(PersonnelCollection.Department, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 编辑科研数据
        /// </summary>
        /// <param name="data">部门数据</param>
        /// <returns></returns>
        public bool EditResearch(Department data)
        {
            if (data.Type != (int)DepartmentType.Type1)
                return false;

            var query = Query.EQ("id", data.Id);
            var update = Update.Set("longitudinalFunds", data.LongitudinalFunds)
                .Set("transverseFunds", data.TransverseFunds)
                .Set("companyFunds", data.CompanyFunds);

            WriteConcernResult result = this.context.Update(PersonnelCollection.Department, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 编辑特殊面积数据
        /// </summary>
        /// <param name="data">部门数据</param>
        /// <returns></returns>
        public bool EditSpecialArea(Department data)
        {
            if (data.Type != (int)DepartmentType.Type1)
                return false;

            var query = Query.EQ("id", data.Id);
            var update = Update.Set("talentArea", data.TalentArea)
                .Set("researchBonusArea", data.ResearchBonusArea)
                .Set("experimentBonusArea", data.ExperimentBonusArea)
                .Set("adjustArea", data.AdjustArea);

            WriteConcernResult result = this.context.Update(PersonnelCollection.Department, query, update);

            if (result.HasLastErrorMessage)
                return false;
            else
                return true;
        }
        #endregion //Method
    }
}
