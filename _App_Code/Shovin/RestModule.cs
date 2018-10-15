using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shovin
{
    public class RestModule : IHttpModule
    {
        static List<string> ServiceMap = new List<string> 
        {
              "xml", 
              "json"
        };

        public void Dispose()
        {
        }

        public void Init(HttpApplication app)
        {
            app.BeginRequest += delegate
            {
                HttpContext ctx = HttpContext.Current;
                string path = ctx.Request.AppRelativeCurrentExecutionFilePath.ToLower();

                foreach (string mapPath in ServiceMap)
                {
                    if (path.Contains("/" + mapPath + "/") || path.EndsWith("/" + mapPath))
                    {
                        string newPath = path.Replace("/" + mapPath + "/", "/" + mapPath + ".svc/");
                        ctx.RewritePath(newPath, null, ctx.Request.QueryString.ToString(), false);
                        return;
                    }
                }
            };
        }

    }
}