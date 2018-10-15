using System;
using System.Web.Mvc;
using Shovin.DB;
using System.Diagnostics;
using AJ.UtiliTools;

namespace Shovin.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["ServiceUrl"] = AJ.UtiliTools.UtiliSetting.AppSetting("ServiceUrl");

            return View();
        }

        public ActionResult Shovin(String tinyUrl)
        {
            ViewData["ServiceUrl"] = AJ.UtiliTools.UtiliSetting.AppSetting("ServiceUrl");

            Result<Shove> shove = Models.ShoveDAL.GetByTinyUrl(tinyUrl);

            if (shove.ErrorCode == 0)
            {
                Models.ShoveDAL.LogVisit(tinyUrl, UserTility.CurrentUserIP, UserTility.UrlReferrer);

                Response.Redirect(shove.Data.FullUrl);
            }

            return View();
        }
    }
}
