using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.Reflection;
using DotNetLive.Framework;
using DotNetLive.AccountWeb.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace DotNetLive.AccountWeb.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private AppSettings _assSettings;

        public HomeController(IHostingEnvironment hostingEnvironment, IOptions<AppSettings> appSettings)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._assSettings = appSettings.Value;
        }

        [Authorize]
        public IActionResult Index()
        {
            //if (_hostingEnvironment.IsDevelopment())
            //{
            //    return View();
            //}
            //else
            //{
            //    return Redirect(_assSettings.MainSite);
            //}
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult EnvInfo()
        {
            var sbContent = new StringBuilder();

            var dicParms = new Dictionary<string, string>();
            var pis = _hostingEnvironment.GetType().GetTypeInfo().GetProperties();
            pis.ForEach(pi => { dicParms.Add(pi.Name, pi.GetValue(_hostingEnvironment, null).ToString()); });

            foreach (var key in dicParms.Keys)
            {
                sbContent.AppendLine($"<strong>{key}</strong>:{dicParms[key]}<br/>");
            }
            return Content(sbContent.ToString());
        }

        const string SessionKeyName = "_Name";
        const string SessionKeyYearsMember = "_YearsMember";
        const string SessionKeyDate = "_Date";

        public IActionResult SessionTest()
        {
            // Requires using Microsoft.AspNetCore.Http;
            HttpContext.Session.SetString(SessionKeyName, "Rick");
            HttpContext.Session.SetInt32(SessionKeyYearsMember, 3);
            return RedirectToAction("SessionNameYears");
        }
        public IActionResult SessionNameYears()
        {
            var name = HttpContext.Session.GetString(SessionKeyName);
            var yearsMember = HttpContext.Session.GetInt32(SessionKeyYearsMember);

            return Content($"Name: \"{name}\",  Membership years: \"{yearsMember}\"");
        }

        public IActionResult SetDate()
        {
            // Requires you add the Set extension method mentioned in the article.
            HttpContext.Session.Set<DateTime>(SessionKeyDate, DateTime.Now);
            return RedirectToAction("GetDate");
        }

        public IActionResult GetDate()
        {
            // Requires you add the Get extension method mentioned in the article.
            var date = HttpContext.Session.Get<DateTime>(SessionKeyDate);
            var sessionTime = date.TimeOfDay.ToString();
            var currentTime = DateTime.Now.TimeOfDay.ToString();

            return Content($"Current time: {currentTime} - "
                         + $"session time: {sessionTime}");
        }
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) :
                                  JsonConvert.DeserializeObject<T>(value);
        }
    }
}
