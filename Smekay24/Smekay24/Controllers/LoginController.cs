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
            var users = db.Users.Where(x => x.Name.Equals(user.Email) && x.Password.Equals(user.Password));
            if (users.Any())
            {
                UserSession.CurrentUser = users.First();
                return RedirectToAction("PersonalCabinetAdverts", "PersonalCabinet");
            }
            else
                return View();
        }

        public ActionResult Logout()
        {
            UserSession.CurrentUser = null;
            UserSession.IsUserLogged = false;
            return RedirectToAction("Index", "Home");
        }
    }
}
