using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockReport.Controllers
{[HandleError]
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            try {
                var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roles = userManager.GetRoles(User.Identity.GetUserId());
            if (roles[0] == "Admin")
            {
                return RedirectToAction("UserList", "Account");
            }else if (roles[0] == "User")
                return RedirectToAction("ClientDetail", "Client");
            }catch(Exception ex)
            {
                return RedirectToAction("HandleError", "Error", new { @msg = ex.Message });
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}