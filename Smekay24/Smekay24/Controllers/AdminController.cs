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
            ViewData["usersCount"] = db.Users.Count();
            ViewData["bannedCount"] = db.Users.Where(x=>x.Banned == 1).Count();
            ViewData["naAdverts"] = db.Advert.Where(x => x.Status == (int)Constants.AdvertStatus.NotAllowed).Count();
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
                Banned = x.Banned == 1 ? "banned" : "",
                BannedMessage = x.Banned == 1 ? "Разбанить" : "Забанить"
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
            model.AddRange(db.Advert.Where(x => x.Status == (int)Constants.AdvertStatus.NotAllowed).Select(x => new AdvertRowData()
            {
                Author = db.Users.FirstOrDefault(u => u.UCode == x.UCode).Email,
                Date = (DateTime)x.Date,
                Title = x.Description,
                Code = x.ACode
            }));
            return View(model);
        }

        public ActionResult Utils()
        {
            ViewData["cities"] = db.City.Select(x => new SelectListItem() { Text = x.Name, Value = x.CCode.ToString() }).ToList();
            ViewData["countries"] = db.Countries.Select(x => new SelectListItem() { Text = x.Country, Value = x.CoCode.ToString() }).ToList();
            ViewData["categories"] = db.Advert_Category.Select(x => new SelectListItem() { Text = x.Name, Value = x.ACCode.ToString() }).ToList();
            return View("Utils");
        }

        [HttpPost]
        public ActionResult AddCity(string name, int cCode)
        {
            City c = new City()
            {
                Name = name,
                CoCode = cCode
            };
            db.City.Add(c);
            db.SaveChanges();
            return Utils();
        }

        [HttpPost]
        public ActionResult DeleteCity(int code)
        {
            var c = db.City.FirstOrDefault(x => x.CCode == code);
            db.City.Remove(c);
            db.SaveChanges();
            return Utils();
        }

        [HttpPost]
        public ActionResult AddCountry(string name)
        {
            Countries c = new Countries()
            {
                Country = name,
            };
            db.Countries.Add(c);
            db.SaveChanges();
            return Utils();
        }

        [HttpPost]
        public ActionResult DeleteCountry(int code)
        {
            var c = db.Countries.FirstOrDefault(x => x.CoCode == code);
            db.Countries.Remove(c);
            db.SaveChanges();
            return Utils();
        }

        [HttpPost]
        public ActionResult AddCategory(string name)
        {
            Advert_Category c = new Advert_Category()
            {
                Name = name,
            };
            db.Advert_Category.Add(c);
            db.SaveChanges();
            return Utils();
        }

        [HttpPost]
        public ActionResult DeleteCategory(int code)
        {
            var c = db.Advert_Category.FirstOrDefault(x => x.ACCode == code);
            db.Advert_Category.Remove(c);
            db.SaveChanges();
            return Utils();
        }
    }
}
