using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Business.Estate;
using Rhea.Data.Personnel;
using Rhea.Model.Estate;
using Rhea.Model.Personnel;

namespace Rhea.Business.Personnel
{
    /// <summary>
    /// 指标计算类
    /// </summary>
    public class MongoIndicatorBusiness : IIndicatorBusiness
    {
        #region Field
        #endregion //Field

        #region Function
        /// <summary>
        /// 学院指标
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        private DepartmentIndicatorModel CollegeIndicator(Department department)
        {
            DepartmentIndicatorModel data = new DepartmentIndicatorModel();
            data.DepartmentId = department.Id;
            data.DepartmentName = department.Name;

            //1 (办公用房+机动用房) * K2
            /* 
             * K2
             * K2=1.2	学院教职工人数＜50人
             * K2=1.1	学院教职工人数50-100人
             * K2=1		学院教职工人数＞100人
             */
            double factorK2 = 0.0;
            if (department.StaffCount < 50)
                factorK2 = 1.2;
            else if (department.StaffCount >= 50 && department.StaffCount <= 100)
                factorK2 = 1.1;
            else
                factorK2 = 1.0;

            /*
             * 办公用房
             * 面积=中级职称及以下教师数×6平方米+副教授数×12平方米+教授数×20平方米+高级教辅人数×12平方米+
             * 中级及以下教辅人数×6平方米+学院党政管理人员数×8平方米+学院处级领导数×20平方米
             */
            data.OfficeArea = department.MediumTeacherCount * 6 + department.AssociateProfessorCount * 12 + department.ProfessorCount * 20 +
                department.AdvanceAssistantCount * 12 + department.MediumAssistantCount * 6 +
                department.PartyLeaderCount * 8 + department.SectionChiefCount * 20;

            /* 
             * 机动用房
             * 面积=125		本科生+研究生+博士生+在编教职工总数≤500
             * 面积=150		500＜本科生+研究生+博士生+在编教职工总数≤1000
             * 面积=175		1000＜本科生+研究生+博士生+在编教职工总数≤2000
             * 面积=200		本科生+研究生+博士生+在编教职工总数＞2000
             */
            int c1 = department.BachelorCount + department.GraduateCount + department.DoctorCount + department.StaffCount;
            if (c1 <= 500)
                data.FlexibleArea = 125;
            else if (c1 > 500 && c1 <= 1000)
                data.FlexibleArea = 150;
            else if (c1 > 1000 && c1 <= 2000)
                data.FlexibleArea = 175;
            else
                data.FlexibleArea = 200;

            /* 
             * 2 实验用房：(学院文理科区分参照excel表AI列)
             * 文科：面积= (本科生数*66/468)*4*系数K1
             * 理科：面积= (本科生数*132/468)*4*系数K1
             */
            if (department.SubjectType == 1)
                data.ExperimentArea = (department.BachelorCount * 66 / 468d) * 4 * department.FactorK1;
            else
                data.ExperimentArea = (department.BachelorCount * 132 / 468d) * 4 * department.FactorK1;
            data.ExperimentArea = Math.Round(data.ExperimentArea, 2);

            /* 
             * 3 研究生用房
             * 面积=研究生人数*4*系数K1
             */
            data.GraduateArea = department.GraduateCount * 4 * department.FactorK1;

            /*
             * 4 博士生用房
             * 面积=博士生人数*6*系数K1
             */
            data.DoctorArea = department.DoctorCount * 6 * department.FactorK1;

            /*
             * 5 工程硕士用房
             * 面积=工程硕士人数*0.4
             */
            data.MasterOfEngineerArea = department.MasterOfEngineerCount * 0.4;

            /*
             * 6 科研用房
             * 年度科研经费=纵向科研经费+横向科研经费×0.65+年度到开发公司账经费×0.3
             * 面积=年度科研经费×2×K3                             年度科研经费≤500
             * 面积=500×2×K3+（年度科研经费-500）*0.8             500＜年度科研经费≤3000
             * 面积=500×2×K3+2500*0.8+（年度科研经费-3000）*0.4   3000＜年度科研经费	
             */
            double totalFunds = department.LongitudinalFunds + department.TransverseFunds * 0.65 + department.CompanyFunds * 0.3;
            if (totalFunds <= 500)
                data.ResearchArea = totalFunds * 2 * department.FactorK3;
            else if (totalFunds > 500 && totalFunds < 3000)
                data.ResearchArea = 500 * 2 * department.FactorK3 + (totalFunds - 500) * 0.8;
            else
                data.ResearchArea = 500 * 2 * department.FactorK3 + 2500 * 0.8 + (totalFunds - 3000) * 0.4;
            data.ResearchArea = Math.Round(data.ResearchArea, 2);

            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var rooms = roomBusiness.GetListByDepartment(department.Id);

            /*
             * 7 对公用房
             * 面积=房屋编码为2.1和3.1的房间面积
             */
            var r21 = rooms.Where(r => r.Function.FirstCode == 2 && r.Function.SecondCode == 1);
            double a21 = Convert.ToDouble(r21.Sum(r => r.UsableArea));
            var r31 = rooms.Where(r => r.Function.FirstCode == 3 && r.Function.SecondCode == 1);
            double a31 = Convert.ToDouble(r31.Sum(r => r.UsableArea));
            data.PublicArea = a21 + a31;

            /*
             * 8 教学补贴用房
             * 面积=房屋编码为2.*（2.1除外）的房间面积
             */
            var r2 = rooms.Where(r => r.Function.FirstCode == 2);
            double a2 = Convert.ToDouble(r2.Sum(r => r.UsableArea));
            data.EducationBonusArea = a2 - a21;

            /*
             * 9 特殊人才用房面积
             */
            data.TalentArea = department.TalentArea;

            /*
             * 10 科研平台补贴
             */
            data.ResearchBonusArea = department.ResearchBonusArea;

            /* 
             * 11 实验教学平台补贴
             */
            data.ExperimentBonusArea = department.ExperimentBonusArea;

            /*
             * 12 调整面积
             */
            data.AdjustArea = department.AdjustArea;

            //应有面积
            data.DeservedArea = (data.OfficeArea + data.FlexibleArea) * factorK2 + data.ExperimentArea +
                data.GraduateArea + data.DoctorArea + data.MasterOfEngineerArea + data.ResearchArea + data.PublicArea +
                data.EducationBonusArea + data.TalentArea + data.ResearchBonusArea + data.ExperimentBonusArea + data.AdjustArea;

            //房间数量
            data.RoomCount = rooms.Count;

            //现有面积
            data.ExistingArea = Convert.ToDouble(rooms.Sum(r => r.UsableArea));

            //定额率
            if (data.DeservedArea == 0)
                data.Overproof = 0;
            else
                data.Overproof = Math.Round(data.ExistingArea / data.DeservedArea * 100, 2);

            return data;
        }

        /// <summary>
        /// 学院指标
        /// </summary>
        /// <param name="department">部门数据</param>
        /// <param name="rooms">房间数据</param>
        /// <returns></returns>
        private DepartmentIndicatorModel CollegeIndicator(Department department, List<Room> rooms)
        {
            DepartmentIndicatorModel data = new DepartmentIndicatorModel();
            data.DepartmentId = department.Id;
            data.DepartmentName = department.Name;

            //1 (办公用房+机动用房) * K2
            /* 
             * K2
             * K2=1.2	学院教职工人数＜50人
             * K2=1.1	学院教职工人数50-100人
             * K2=1		学院教职工人数＞100人
             */
            double factorK2 = 0.0;
            if (department.StaffCount < 50)
                factorK2 = 1.2;
            else if (department.StaffCount >= 50 && department.StaffCount <= 100)
                factorK2 = 1.1;
            else
                factorK2 = 1.0;

            /*
             * 办公用房
             * 面积=中级职称及以下教师数×6平方米+副教授数×12平方米+教授数×20平方米+高级教辅人数×12平方米+
             * 中级及以下教辅人数×6平方米+学院党政管理人员数×8平方米+学院处级领导数×20平方米
             */
            data.OfficeArea = department.MediumTeacherCount * 6 + department.AssociateProfessorCount * 12 + department.ProfessorCount * 20 +
                department.AdvanceAssistantCount * 12 + department.MediumAssistantCount * 6 +
                department.PartyLeaderCount * 8 + department.SectionChiefCount * 20;

            /* 
             * 机动用房
             * 面积=125		本科生+研究生+博士生+在编教职工总数≤500
             * 面积=150		500＜本科生+研究生+博士生+在编教职工总数≤1000
             * 面积=175		1000＜本科生+研究生+博士生+在编教职工总数≤2000
             * 面积=200		本科生+研究生+博士生+在编教职工总数＞2000
             */
            int c1 = department.BachelorCount + department.GraduateCount + department.DoctorCount + department.StaffCount;
            if (c1 <= 500)
                data.FlexibleArea = 125;
            else if (c1 > 500 && c1 <= 1000)
                data.FlexibleArea = 150;
            else if (c1 > 1000 && c1 <= 2000)
                data.FlexibleArea = 175;
            else
                data.FlexibleArea = 200;

            /* 
             * 2 实验用房：(学院文理科区分参照excel表AI列)
             * 文科：面积= (本科生数*66/468)*4*系数K1
             * 理科：面积= (本科生数*132/468)*4*系数K1
             */
            if (department.SubjectType == 1)
                data.ExperimentArea = (department.BachelorCount * 66 / 468d) * 4 * department.FactorK1;
            else
                data.ExperimentArea = (department.BachelorCount * 132 / 468d) * 4 * department.FactorK1;
            data.ExperimentArea = Math.Round(data.ExperimentArea, 2);

            /* 
             * 3 研究生用房
             * 面积=研究生人数*4*系数K1
             */
            data.GraduateArea = department.GraduateCount * 4 * department.FactorK1;

            /*
             * 4 博士生用房
             * 面积=博士生人数*6*系数K1
             */
            data.DoctorArea = department.DoctorCount * 6 * department.FactorK1;

            /*
             * 5 工程硕士用房
             * 面积=工程硕士人数*0.4
             */
            data.MasterOfEngineerArea = department.MasterOfEngineerCount * 0.4;

            /*
             * 6 科研用房
             * 年度科研经费=纵向科研经费+横向科研经费×0.65+年度到开发公司账经费×0.3
             * 面积=年度科研经费×2×K3                             年度科研经费≤500
             * 面积=500×2×K3+（年度科研经费-500）*0.8             500＜年度科研经费≤3000
             * 面积=500×2×K3+2500*0.8+（年度科研经费-3000）*0.4   3000＜年度科研经费	
             */
            double totalFunds = department.LongitudinalFunds + department.TransverseFunds * 0.65 + department.CompanyFunds * 0.3;
            if (totalFunds <= 500)
                data.ResearchArea = totalFunds * 2 * department.FactorK3;
            else if (totalFunds > 500 && totalFunds < 3000)
                data.ResearchArea = 500 * 2 * department.FactorK3 + (totalFunds - 500) * 0.8;
            else
                data.ResearchArea = 500 * 2 * department.FactorK3 + 2500 * 0.8 + (totalFunds - 3000) * 0.4;
            data.ResearchArea = Math.Round(data.ResearchArea, 2);           

            /*
             * 7 对公用房
             * 面积=房屋编码为2.1和3.1的房间面积
             */
            var r21 = rooms.Where(r => r.Function.FirstCode == 2 && r.Function.SecondCode == 1);
            double a21 = Convert.ToDouble(r21.Sum(r => r.UsableArea));
            var r31 = rooms.Where(r => r.Function.FirstCode == 3 && r.Function.SecondCode == 1);
            double a31 = Convert.ToDouble(r31.Sum(r => r.UsableArea));
            data.PublicArea = a21 + a31;

            /*
             * 8 教学补贴用房
             * 面积=房屋编码为2.*（2.1除外）的房间面积
             */
            var r2 = rooms.Where(r => r.Function.FirstCode == 2);
            double a2 = Convert.ToDouble(r2.Sum(r => r.UsableArea));
            data.EducationBonusArea = a2 - a21;

            /*
             * 9 特殊人才用房面积
             */
            data.TalentArea = department.TalentArea;

            /*
             * 10 科研平台补贴
             */
            data.ResearchBonusArea = department.ResearchBonusArea;

            /* 
             * 11 实验教学平台补贴
             */
            data.ExperimentBonusArea = department.ExperimentBonusArea;

            /*
             * 12 调整面积
             */
            data.AdjustArea = department.AdjustArea;

            //应有面积
            data.DeservedArea = (data.OfficeArea + data.FlexibleArea) * factorK2 + data.ExperimentArea +
                data.GraduateArea + data.DoctorArea + data.MasterOfEngineerArea + data.ResearchArea + data.PublicArea +
                data.EducationBonusArea + data.TalentArea + data.ResearchBonusArea + data.ExperimentBonusArea + data.AdjustArea;
            
            //房间数量
            data.RoomCount = rooms.Count;

            //现有面积
            data.ExistingArea = Convert.ToDouble(rooms.Sum(r => r.UsableArea));

            //定额率
            if (data.DeservedArea == 0)
                data.Overproof = 0;
            else
                data.Overproof = Math.Round(data.ExistingArea / data.DeservedArea * 100, 2);

            return data;
        }

        /// <summary>
        /// 部门指标
        /// </summary>
        /// <param name="department">部门数据</param>
        /// <returns></returns>
        private DepartmentIndicatorModel InstitutionIndicator(Department department)
        {
            DepartmentIndicatorModel data = new DepartmentIndicatorModel();
            data.DepartmentId = department.Id;
            data.DepartmentName = department.Name;

            //1、正校长（书记）50平方/人，副校长（书记）40平方/人。
            data.PresidentArea = department.PresidentCount * 50 + department.VicePresidentCount * 40;

            //2、部门正职25平方/人，部门副职18平方/人，部门其它人员8平方/人。部门人员少于3人的，该科室按20平方配置。
            if (department.ChiefCount + department.ViceChiefCount + department.MemberCount < 3)
                data.SectionArea = 20;
            else
                data.SectionArea = department.ChiefCount * 25 + department.ViceChiefCount * 18 + department.MemberCount * 8;

            //3、部门业务用房2平方/人，不足10人的部门按20平方算。
            int totalPerson = department.PresidentCount + department.VicePresidentCount +
                department.ChiefCount + department.ViceChiefCount + department.MemberCount;
            if (totalPerson < 10)
                data.BusinessArea = 20;
            else
                data.BusinessArea = totalPerson * 2;

            data.DeservedArea = data.PresidentArea + data.SectionArea + data.BusinessArea;
            
            IRoomBusiness roomBusiness = new MongoRoomBusiness();
            var rooms = roomBusiness.GetListByDepartment(department.Id);

            //房间数量
            data.RoomCount = rooms.Count;

            //现有面积
            data.ExistingArea = Convert.ToDouble(rooms.Sum(r => r.UsableArea));

            //定额率
            if (data.DeservedArea == 0)
                data.Overproof = 0;
            else
                data.Overproof = Math.Round(data.ExistingArea / data.DeservedArea * 100, 2);

            return data;
        }

        /// <summary>
        /// 部门指标
        /// </summary>
        /// <param name="department">部门数据</param>
        /// <param name="rooms">房间数据</param>
        /// <returns></returns>
        private DepartmentIndicatorModel InstitutionIndicator(Department department, List<Room> rooms)
        {
            DepartmentIndicatorModel data = new DepartmentIndicatorModel();
            data.DepartmentId = department.Id;
            data.DepartmentName = department.Name;

            //1、正校长（书记）50平方/人，副校长（书记）40平方/人。
            data.PresidentArea = department.PresidentCount * 50 + department.VicePresidentCount * 40;

            //2、部门正职25平方/人，部门副职18平方/人，部门其它人员8平方/人。部门人员少于3人的，该科室按20平方配置。
            if (department.ChiefCount + department.ViceChiefCount + department.MemberCount < 3)
                data.SectionArea = 20;
            else
                data.SectionArea = department.ChiefCount * 25 + department.ViceChiefCount * 18 + department.MemberCount * 8;

            //3、部门业务用房2平方/人，不足10人的部门按20平方算。
            int totalPerson = department.PresidentCount + department.VicePresidentCount +
                department.ChiefCount + department.ViceChiefCount + department.MemberCount;
            if (totalPerson < 10)
                data.BusinessArea = 20;
            else
                data.BusinessArea = totalPerson * 2;

            data.DeservedArea = data.PresidentArea + data.SectionArea + data.BusinessArea;

            //房间数量
            data.RoomCount = rooms.Count;

            //现有面积
            data.ExistingArea = Convert.ToDouble(rooms.Sum(r => r.UsableArea));

            //定额率
            if (data.DeservedArea == 0)
                data.Overproof = 0;
            else
                data.Overproof = Math.Round(data.ExistingArea / data.DeservedArea * 100, 2);

            return data;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 部门指标计算
        /// </summary>
        /// <param name="department">部门数据</param>
        /// <returns></returns>
        public DepartmentIndicatorModel GetDepartmentIndicator(Department department)
        {
            if (department.Type == (int)DepartmentType.Type1)
            {
                DepartmentIndicatorModel data = CollegeIndicator(department);
                return data;
            }
            else
            {
                DepartmentIndicatorModel data = InstitutionIndicator(department);
                return data;
            }
        }

        /// <summary>
        /// 部门指标计算
        /// </summary>
        /// <param name="department">部门数据</param>
        /// <param name="rooms">房间数据</param>
        /// <returns></returns>
        public DepartmentIndicatorModel GetDepartmentIndicator(Department department, List<Room> rooms)
        {
            if (department.Type == (int)DepartmentType.Type1)
            {
                DepartmentIndicatorModel data = CollegeIndicator(department, rooms);
                return data;
            }
            else
            {
                DepartmentIndicatorModel data = InstitutionIndicator(department, rooms);
                return data;
            }
        }
        #endregion //Method
    }
}
