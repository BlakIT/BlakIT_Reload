using Smekay24.Models;
using Smekay24.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smekay24.Controllers
{
    public class PersonalCabinetController : Controller
    {

        private SmekayEntities db = new SmekayEntities();

        //
        // GET: /PersonalCabinet/
        public ActionResult PersonalCabinetAdverts()
        {
            var items = db.Advert.Where(x => x.UCode == UserSession.CurrentUser.UCode).ToList();

            foreach (Advert adv in items)
            {
                var image = (from imToAdv in db.Images_To_Advert
                             join img in db.Images on imToAdv.ICode equals img.ICode
                             where imToAdv.ACode == adv.ACode
                             select img.Url).FirstOrDefault();

                if (image == null)
                    adv.History = "/images/static.jpg";
                else
                    adv.History = image.Substring(1);
            }

            return View(items);
        }

        [HttpGet]
        public ActionResult PersonalCabinetSettings()
        {
            ViewData["cities"] = db.City.Select(x => new SelectListItem() { Text = x.Name, Value = x.CCode.ToString() }).ToList();
            var user = db.Users.First(x => x.UCode == UserSession.CurrentUser.UCode);
            ViewData["user"] = user;
            PCSettingsModel model = new PCSettingsModel();
            model.Name = user.Name;
            model.Phones = user.Phone;
            model.City = user.CCode.ToString();
            model.News = user.News == 1;
            model.Notifications = user.Notifications == 1;
            model.Reminders = user.Reminders == 1;
            model.Contact = user.Contacts;
            return View(model);
        }

        [HttpPost]
        public ActionResult PersonalCabinetSettings(PCSettingsModel form)
        {
            var user = db.Users.First(x => x.UCode == UserSession.CurrentUser.UCode);
            user.Name = form.Name;
            user.Phone = form.Phones;
            user.CCode = Int32.Parse(form.City);
            user.Contacts = form.Contact;
            user.News = form.News ? 1 : 0;
            user.Notifications = form.Notifications ? 1 : 0;
            user.Reminders = form.Reminders ? 1 : 0;
            if (UserSession.CheckPassword(user.Email, form.CurrentPassword)!=null && form.NewPassword.Equals(form.ConfirmNewPass))
            {
                user.Password = form.NewPassword;
            }
            db.SaveChanges();
            ViewData["cities"] = db.City.Select(x => new SelectListItem() { Text = x.Name, Value = x.CCode.ToString() }).ToList();
            return View(form);
        }

        public ActionResult PersonalCabinetNotification()
        {
            var items = db.Notification.Where(x => x.RecipientCode == Smekay24.WebAPI.UserSession.CurrentUser.UCode).ToList();

            return View(items);
        }

        public ActionResult ApplyNotification(int idNot)
        {
            var item = db.Notification.Where(x => x.NCode == idNot).FirstOrDefault();

            Users author = db.Users.Where(x => x.UCode == item.AuthorCode).FirstOrDefault();

            item.Content = "+" + item.Content + "Вы приняли предложение! Для согласования свяжитесь с пользователем " + author.Name + " ( Е-мэйл - " + author.Email + " | Телефон - " + author.Phone + ")";
                
            db.SaveChanges();

            return RedirectToAction("PersonalCabinetNotification");
        }

        public ActionResult DenyNotification(int idNot)
        {
            var item = db.Notification.Where(x => x.NCode == idNot).FirstOrDefault();

            db.Notification.Remove(item);
            db.SaveChanges();

            return RedirectToAction("PersonalCabinetNotification");
        }

        public ActionResult PersonalCabinetDelete()
        {
            var user = db.Users.First(x => x.UCode == UserSession.CurrentUser.UCode);
            db.Users.Remove(user);
            db.SaveChanges();
            UserSession.CurrentUser = null;
            UserSession.IsUserLogged = false;
            return RedirectToAction("Index", "Home", null);
        }

    }
}
