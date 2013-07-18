using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rhea.Business.Account;

namespace Rhea.UI.Areas.Admin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {        
        #region Field       
        /// <summary>
        /// 管理业务
        /// </summary>
        private IAccountBusiness accountBusiness;
        #endregion //Field

        #region Function
        protected override void Initialize(RequestContext requestContext)
        {
            if (accountBusiness == null)
            {
                accountBusiness = new MongoAccountBusiness();
            }

            base.Initialize(requestContext);
        }
        #endregion //Function

        #region Action
      
        #endregion //Action
    }
}
