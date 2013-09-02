using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Rhea.Data.Server;

namespace Rhea.Business.Personnel
{
    /// <summary>
    /// 人事字典
    /// </summary>
    public class PersonnelDictionaryBusiness : DictionaryBusiness
    {
        #region Field

        #endregion //Field

        #region Constructor
        /// <summary>
        /// 人事字典
        /// </summary>
        public PersonnelDictionaryBusiness()
            : base(RheaServer.PersonnelDatabase, PersonnelCollection.Dictionary)
        {
        }
        #endregion //Constructor

        #region Method
        #endregion //Method
    }
}
