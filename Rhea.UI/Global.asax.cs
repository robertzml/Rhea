using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Rhea.UI
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpContext ctx = app.Context;          //获取本次Http请求的HttpContext对象  
            if (ctx.User != null)
            {
                if (ctx.Request.IsAuthenticated == true)    //验证过的一般用户才能进行角色验证  
                {
                    System.Web.Security.FormsIdentity fi = (System.Web.Security.FormsIdentity)ctx.User.Identity;
                    System.Web.Security.FormsAuthenticationTicket ticket = fi.Ticket;       //取得身份验证票  
                    string userData = ticket.UserData;                                      //从UserData中恢复role信息
                    string[] roles = userData.Split(',');                                   //将角色数据转成字符串数组,得到相关的角色信息  
                    ctx.User = new System.Security.Principal.GenericPrincipal(fi, roles);   //这样当前用户就拥有角色信息了                    
                }
            }
        }
    }
}