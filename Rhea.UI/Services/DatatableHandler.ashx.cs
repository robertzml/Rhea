using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Rhea.Business;
using Rhea.Common;
using Rhea.Model;

namespace Rhea.UI.Services
{
    /// <summary>
    /// DatatableHandler 的摘要说明
    /// </summary>
    public class DatatableHandler : IHttpHandler
    {
        #region Field
        private readonly JavaScriptSerializer js;

        private DatatableRequest request;

        private DatatableResponse response;
        #endregion //Field

        #region Function
        /// <summary>
        /// 处理参数
        /// </summary>
        /// <param name="context"></param>
        private void HandleParams(HttpContext context)
        {
            this.request = new DatatableRequest();
            request.draw = Convert.ToInt32(context.Request.Params["draw"]);
            request.start = Convert.ToInt32(context.Request.Params["start"]);
            request.length = Convert.ToInt32(context.Request.Params["length"]);
            request.search = context.Request.Params["search[value]"];
            request.isRegex = Convert.ToBoolean(context.Request.Params["search[regex]"]);
            request.columns = new List<Dictionary<string, object>>();
            request.order = new List<Dictionary<string, object>>();

            int index = 0;
            while (context.Request.Params[string.Format("columns[{0}][data]", index)] != null)
            {
                Dictionary<string, object> col = new Dictionary<string, object>();
                col.Add("data", context.Request.Params[string.Format("columns[{0}][data]", index)]);
                col.Add("name", context.Request.Params[string.Format("columns[{0}][name]", index)]);
                col.Add("orderable", Convert.ToBoolean(context.Request.Params[string.Format("columns[{0}][orderable]", index)]));
                col.Add("searchable", Convert.ToBoolean(context.Request.Params[string.Format("columns[{0}][searchable]", index)]));
                col.Add("searchvalue", context.Request.Params[string.Format("columns[{0}][search][value]", index)]);
                col.Add("searchregex", Convert.ToBoolean(context.Request.Params[string.Format("columns[{0}][search][regex]", index)]));

                request.columns.Add(col);
                index++;
            }

            index = 0;

            while (context.Request.Params[string.Format("order[{0}][column]", index)] != null)
            {
                Dictionary<string, object> ord = new Dictionary<string, object>();
                ord.Add("column", context.Request.Params[string.Format("order[{0}][column]", index)]);
                ord.Add("dir", context.Request.Params[string.Format("order[{0}][dir]", index)]);

                request.order.Add(ord);
                index++;
            }
        }

        /// <summary>
        /// 处理操作
        /// </summary>
        /// <param name="context"></param>
        private void HandleAction(HttpContext context)
        {
            string action = context.Request.Params["action"];

            if (action == "log")
            {
                GetLog(context);
            }
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <returns></returns>
        private void GetLog(HttpContext context)
        {
            LogBusiness logBusiness = new LogBusiness();
            var data = logBusiness.Get(this.request.start, this.request.length);
            int count = (int)logBusiness.Count();

            this.response = new DatatableResponse
            {
                draw = this.request.draw,
                recordsTotal = count,
                recordsFilterd = count,
                data = data.Select(g => new { g._id, g.Title, g.Time, g.UserName, TypeName = ((LogType)g.Type).DisplayName(), g.Type }).ToArray()
            };
        }

        private void WriteJsonIframeSafe(HttpContext context)
        {
            context.Response.AddHeader("Vary", "Accept");
            try
            {
                if (context.Request["HTTP_ACCEPT"].Contains("application/json"))
                    context.Response.ContentType = "application/json";
                else
                    context.Response.ContentType = "text/plain";
            }
            catch
            {
                context.Response.ContentType = "text/plain";
            }

            
            var jsonObj = js.Serialize(this.response);
            context.Response.Write(jsonObj);
        }
        #endregion //Function

        #region Method
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            HandleParams(context);
            HandleAction(context);

            WriteJsonIframeSafe(context);
        }
        #endregion //Method

        #region Property
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion //Property
    }

    class DatatableRequest
    {
        public int draw { get; set; }

        public int start { get; set; }

        public int length { get; set; }

        public string search { get; set; }

        public bool isRegex { get; set; }

        public List<Dictionary<string, object>> columns;

        public List<Dictionary<string, object>> order;
    }

    class DatatableResponse
    {
        public int draw { get; set; }

        public int recordsTotal { get; set; }

        public int recordsFilterd { get; set; }

        public object data { get; set; }
    }
}