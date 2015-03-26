using Smekay24.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smekay24.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserData user)
        {
            SmekayEntities db = new SmekayEntities();
            
            if (db.Users.Where(x=>x.Name.Equals(user.Name) && x.Password.Equals(user.Password)).Any())
            {
                return RedirectToAction("Index", "Home");
            }
            else
                return View();
        }

    }
}
