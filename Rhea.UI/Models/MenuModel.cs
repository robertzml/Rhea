using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rhea.Data.Entities;

namespace Rhea.UI.Models
{
    public class MenuModel
    {
        public List<Department> Departments { get; set; }

        public List<BuildingGroup> BuildingGroups { get; set; }
    }
}