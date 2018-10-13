using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace QTasMarketing.Web.Controllers
{
   
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}