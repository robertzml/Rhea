using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhea.Data;
using Rhea.Data.Mongo;
using Rhea.Model;

namespace Rhea.Business
{
    /// <summary>
    /// 字典业务
    /// </summary>
    public class DictionaryBusiness
    {
        #region Field
        private IDictionaryRepository dictionaryRepository;
        #endregion //Field

        #region Constructor
        public DictionaryBusiness()
        {
            this.dictionaryRepository = new MongoDictionaryRepository();
        }
        #endregion //Constructor

        #region Method
        public IEnumerable<Dictionary> Get()
        {
            var data = this.dictionaryRepository.Get();
            return data;
        }
        #endregion //Method
    }
}
