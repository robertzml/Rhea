using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhea.Data.Personnel;
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

        #region Method
        /// <summary>
        /// 部门指标计算(限学院)
        /// </summary>
        /// <param name="department">部门数据</param>
        /// <returns></returns>
        public DepartmentIndicatorModel GetDepartmentIndicator(Department department)
        {
            if (department.Type != (int)DepartmentType.Type1)
                return null;

            DepartmentIndicatorModel data = new DepartmentIndicatorModel();
            data.DepartmentId = department.Id;
            data.DepartmentName = department.Name;

            //面积=中级职称及以下教师数×6平方米+副教授数×12平方米+教授数×18平方米+高级教辅人数×12平方米+中级及以下教辅人数×6平方米+
            //学院党政管理人员数×6平方米+学院处级领导数×16平方米
            data.OfficeArea = department.MediumTeacherCount * 6 + department.AssociateProfessorCount * 12 + department.ProfessorCount * 18 +
                department.AdvanceAssistantCount * 12 + department.MediumAssistantCount * 6 + 
                department.PartyLeaderCount * 6 + department.SectionChiefCount * 16;

            /*面积=200		本科生+研究生+博士生+在编教职工总数≤500
            面积=225		500＜本科生+研究生+博士生+在编教职工总数≤1000
            面积=250		1000＜本科生+研究生+博士生+在编教职工总数≤2000
            面积=275		本科生+研究生+博士生+在编教职工总数＞2000*/
            int c1 = department.BachelorCount + department.GraduateCount + department.DoctorCount + department.StaffCount;
            if (c1 <= 500)
                data.FlexibleArea = 200;
            else if (c1 > 500 && c1 <= 1000)
                data.FlexibleArea = 225;
            else if (c1 > 1000 && c1 <= 2000)
                data.FlexibleArea = 250;
            else
                data.FlexibleArea = 275;

            /*实验用房：(学院文理科区分参照excel表AI列)
	            文科：面积= (本科生数*66/468)*4*系数K1
	            理科：面积= (本科生数*132/468)*4*系数K1*/


            return data;
        }
        #endregion //Method
    }
}
