using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rhea.UI.Services
{
    public static class CommonService
    {
        public static string ErrorPage(int code)
        {            
            return "/403.html";
        }
    }
}