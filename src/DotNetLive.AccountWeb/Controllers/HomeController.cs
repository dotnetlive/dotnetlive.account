using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.Reflection;
using DotNetLive.Framework;
using DotNetLive.AccountWeb.Configurations;
using Microsoft.Extensions.Options;

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

        public IActionResult Index()
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                return View();
            }
            else
            {
                return Redirect(_assSettings.MainSite);
            }
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
    }
}
