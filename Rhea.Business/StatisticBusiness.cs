using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Business.Estate;
using Rhea.Model;
using Rhea.Model.Estate;

namespace Rhea.Business
{
    public class StatisticBusiness
    {
        /// <summary>
        /// 根据建筑获取二级分类面积
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <param name="firstCode">一级分类编码</param>
        /// <param name="functionCodes">分类项</param>
        /// <param name="rooms">房间列表</param>
        /// <returns></returns>
        public List<RoomSecondClassifyAreaModel> GetBuildingSecondClassifyArea(int buildingId, int firstCode, List<RoomFunctionCode> functionCodes, IEnumerable<Room> rooms)
        {
            //var result = from r in rooms
            //             where r.Function.FirstCode == firstCode 
            //             group r by r.Function.SecondCode into g
            //             select new { g.Key, Count = g.Count(), Area = g.Sum(t => t.UsableArea) };
            var result = rooms.GroupBy(r => r.Function.SecondCode).Select(g => new { g.Key, Count = g.Count(), Area = g.Sum(t => t.UsableArea) });

            List<RoomSecondClassifyAreaModel> model = new List<RoomSecondClassifyAreaModel>();
            foreach (var item in functionCodes.Where(r => r.FirstCode == firstCode))
            {
                RoomSecondClassifyAreaModel m = new RoomSecondClassifyAreaModel();
                m.FirstCode = firstCode;
                m.SecondCode = item.SecondCode;
                m.Property = item.FunctionProperty;

                var single = result.SingleOrDefault(r => r.Key == item.SecondCode);
                if (single == null)
                {
                    m.RoomCount = 0;
                    m.Area = 0;
                }
                else
                {
                    m.RoomCount = single.Count;
                    m.Area = Math.Round(single.Area, RheaConstant.AreaDecimalDigits);
                }

                model.Add(m);
            }

            return model.OrderBy(r => r.SecondCode).ToList();
        }

        /// <summary>
        /// 获取建筑房间一级分类面积
        /// </summary>
        /// <param name="buildingId">建筑ID</param>
        /// <param name="firstCode">一级分类编码</param>
        /// <param name="functionCodes">分类项</param>
        /// <param name="rooms">房间列表</param>
        /// <param name="sortSecond">二级分类是否按面积排序</param>
        /// <returns></returns>
        public RoomFirstClassifyAreaModel GetBuildingFirstClassifyArea(int buildingId, int firstCode, List<RoomFunctionCode> functionCodes, IEnumerable<Room> rooms, bool sortSecond = false)
        {
            RoomFirstClassifyAreaModel model = new RoomFirstClassifyAreaModel();

            model.FirstCode = firstCode;
            model.ClassifyName = functionCodes.Where(r => r.FirstCode == firstCode).First().ClassifyName;
            model.SecondClassify = GetBuildingSecondClassifyArea(buildingId, firstCode, functionCodes, rooms.Where(r => r.Function.FirstCode == firstCode));
            model.RoomCount = model.SecondClassify.Sum(r => r.RoomCount);
            model.Area = Math.Round(model.SecondClassify.Sum(r => r.Area), RheaConstant.AreaDecimalDigits);

            if (sortSecond)
                model.SecondClassify = model.SecondClassify.OrderByDescending(r => r.Area).ToList();

            return model;
        }
    }
}
