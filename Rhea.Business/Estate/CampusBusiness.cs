using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Model.Estate;
using Rhea.Data;
using Rhea.Data.Mongo;

namespace Rhea.Business.Estate
{
    public class CampusBusiness
    {
        public List<Campus> Get()
        {
            //IRepository<Campus> campusRepository = new MongoRepository<Campus>();
            //return campusRepository.ToList();

            return null;
        }

        public Campus Get(int id)
        {
            //IRepository<Campus> campusRepository = new MongoRepository<Campus>();

            //return campusRepository.GetById(id.ToString());

            return null;
        }
    }
}
