using Smekay24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smekay24.Controllers
{
    public class AdvertController : Controller
    {
        private SmekayEntities db = new SmekayEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAdvertView()
        {
            return View();
        }

        public ActionResult AddAdvert()
        {
            SetViewDataCollections();

            return View();
        }

        [HttpPost]
        public ActionResult AddAdvert(Advert advert, IEnumerable<HttpPostedFileBase> fileUpload)
        {
            advert.Date = DateTime.Now;
            advert.UCode = Smekay24.WebAPI.UserSession.CurrentUser.UCode;
            advert.Status = (int)Constants.AdvertStatus.NotAllowed;

            db.Advert.Add(advert);
            db.SaveChanges();

            foreach (var img in fileUpload)
            {
                if (img != null)
                {
                    string fileName = "~/advertImage/" + System.IO.Path.GetFileName(img.FileName);

                    img.SaveAs(Server.MapPath(fileName));

                    Images imgdb = new Images() { Url = fileName };

                    db.Images.Add(imgdb);
                    db.SaveChanges();

                    db.Images_To_Advert.Add(new Images_To_Advert() { ACode = advert.ACode, ICode = imgdb.ICode });
                }
            }


            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetAdvertsFromCategory(string catText)
        {
            int catId = Int32.Parse(
                db.Advert_Category.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.ACCode.ToString()
                }).ToList().FirstOrDefault(x => x.Text == catText).Value);

            SetViewDataCollections();

            var items = db.Advert.Where(x => x.ACCode == catId && x.Status == (int)Constants.AdvertStatus.Allowed).ToList();

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

        public ActionResult GetAdvertsByLetter(string letter)
        {
            SetViewDataCollections();

            var lettersID = db.Advert.Select(x => new { Letter = x.Title.Substring(0, 1).ToUpper(), ID = x.ACode }).ToList();
            letter = letter.ToUpper();

            var IDs = lettersID.Where(x => x.Letter == letter).Select(x => x.ID).ToList();

            var items = db.Advert.Where(x => IDs.Contains(x.ACode) && x.Status == (int)Constants.AdvertStatus.Allowed).ToList();

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

            return View("GetAdvertsFromCategory", items);
        }

        public ActionResult GetAdvertsBySearch(string search, int accode)
        {
            SetViewDataCollections();

            search = search.ToUpper();

            List<Advert> adverts = db.Advert.Where(x => x.ACCode == accode && x.Status == (int)Constants.AdvertStatus.Allowed).ToList();

            var items = adverts.Where(x => x.Title != null && x.Title.ToUpper().Contains(search)).ToList();

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

            return View("GetAdvertsFromCategory", items);
        }

        public ActionResult GetAdvertByID(int id)
        {
            SetViewDataCollections();

            Advert adv = db.Advert.FirstOrDefault(x => x.ACode == id);

            GetImages(id);

            return View("GetAdvert", adv);
        }

        public ActionResult AllowAdvert(int code)
        {
            SetViewDataCollections();

            Advert adv = db.Advert.FirstOrDefault(x => x.ACode == code);
            adv.Status = (int)Constants.AdvertStatus.Allowed;
            db.SaveChanges();

            GetImages(adv.ACode);

            return View("GetAdvert", adv);
        }

        public ActionResult ForbidAdvert(int code)
        {
            SetViewDataCollections();

            Advert adv = db.Advert.FirstOrDefault(x => x.ACode == code);
            adv.Status = (int)Constants.AdvertStatus.NotAllowed;
            db.SaveChanges();

            GetImages(adv.ACode);

            return View("GetAdvert", adv);
        }

        private void GetImages(int id)
        {
            Advert adv = db.Advert.FirstOrDefault(x => x.ACode == id);

            if (adv == null)
                return;

            var images = (from imToAdv in db.Images_To_Advert
                          join img in db.Images on imToAdv.ICode equals img.ICode
                          where imToAdv.ACode == adv.ACode
                          select img.Url).ToList();

            for (int i = 0; i < 6; i++)
            {
                if (i < images.Count)
                    images[i] = images[i].Substring(1);
                else
                    images.Add("/images/static.jpg");
            }

            ViewData["Images"] = images;

        }

        //-------------------------- Send Message

        public ActionResult SendNotification(int advertID, string price)
        {
            Advert advert = db.Advert.Where(x => x.ACode == advertID).FirstOrDefault();

            Notification notific = new Notification()
            {
                AuthorCode = Smekay24.WebAPI.UserSession.CurrentUser.UCode,
                RecipientCode = advert.UCode,
                Content = "Пользователь " + Smekay24.WebAPI.UserSession.CurrentUser.Name + " предлагает Вам " + price + " за " + advert.Title
            };

            db.Notification.Add(notific);
            db.SaveChanges();

            return Json(true);
        }

        #region Helper Methods

        private void SetViewDataCollections()
        {
            ViewData["alphabetic"] = new string[] { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            ViewData["category"] = db.Advert_Category.Select(x => new SelectListItem() { Text = x.Name, Value = x.ACCode.ToString() }).ToList();
            ViewData["cities"] = db.City.Select(x => new SelectListItem() { Text = x.Name, Value = x.CCode.ToString() }).ToList();
        }

        #endregion
    }
}
