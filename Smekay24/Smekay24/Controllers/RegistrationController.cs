using Smekay24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smekay24.Controllers
{
    public class RegistrationController : Controller
    {
        private SmekayEntities db = new SmekayEntities();
        [HttpGet]
        public ActionResult Registration()
        {
            ViewData["cities"] = db.City.Select(x => new SelectListItem() { Text = x.Name, Value = x.CCode.ToString() }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegistrationFormModel form)
        {
            if (form.Password == form.ConfirmPassword)
            {
                if (!db.Users.Any(x => x.Email == form.Email))
                {
                    var user = new Users()
                    {
                        Name = form.Name,
                        Phone = form.Phones,
                        Email = form.Email,
                        Reminders = Convert.ToInt32(form.IsRemindersAssigned),
                        Notifications = Convert.ToInt32(form.IsNotitifcationAssigned),
                        News = Convert.ToInt32(form.IsNewsAssigned),
                        Password = form.Password,
                        CCode = Int32.Parse(form.City)
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                    Session["CurrentUser"] = user;
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Registration();
            }
        }

    }
}
