using Smekay24.Models;
using Smekay24.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smekay24.Controllers
{
    public class AdminController : Controller
    {
        private SmekayEntities db = new SmekayEntities();

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult AdminUsers()
        {
            UserFormModel model = new UserFormModel();
            model.Rows = db.Users.Select(x => new UserRowData()
            {
                Email = x.Email,
                Name = x.Name,
                City = db.City.FirstOrDefault(c => c.CCode == x.CCode).Name,
                Code = x.UCode,
                Contacts = x.Contacts,
                Phones = x.Phone,
                Banned = x.Banned == 1?"banned":"" ,
                BannedMessage = x.Banned ==1?"Разбанить":"Забанить"
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult EditUser(int code)
        {
            return View();
        }

        [HttpGet]
        public ActionResult BanUser(int code)
        {
            var user = db.Users.FirstOrDefault(x => x.UCode == code);
            user.Banned = user.Banned == 1 ? 0 : 1;
            db.SaveChanges();
            return RedirectToAction("AdminUsers", "Admin", null);
        }

        [HttpGet]
        public ActionResult DeleteUser(int code)
        {
            db.Users.Remove(db.Users.FirstOrDefault(x => x.UCode == code));
            db.SaveChanges();
            return RedirectToAction("AdminUsers", "Admin", null);
        }

        public ActionResult AdminAdverts()
        {
            List<AdvertRowData> model = new List<AdvertRowData>();
            model.AddRange(db.Advert.Where(x => x.Status == (int)Constants.AdvertStatus.NotAllowed).Select(x => new AdvertRowData() { 
            Author = db.Users.FirstOrDefault(u=>u.UCode == x.UCode).Email,
            Date = (DateTime)x.Date,
            Title = x.Description,
            Code = x.ACode
            }));
            return View(model);
        }

    }
}
