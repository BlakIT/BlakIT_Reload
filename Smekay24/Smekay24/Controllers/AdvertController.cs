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

            ViewData["cities"] = db.City.Select(x => new SelectListItem() { Text = x.Name, Value = x.CCode.ToString() }).ToList();
            ViewData["category"] = db.Advert_Category.Select(x => new SelectListItem() { Text = x.Name, Value = x.ACCode.ToString() }).ToList(); 

            return View();
        }

        [HttpPost]
        public ActionResult AddAdvert(Advert advert, IEnumerable<HttpPostedFileBase> fileUpload)
        {
            foreach (var img in fileUpload)
            {
                if (img != null)
                {
                    string fileName = System.IO.Path.GetFileName(img.FileName);
                    //img.SaveAs(Server.MapPath("~/Files/" + fileName));
                }
            }

            db.Advert.Add(advert);
            db.SaveChanges();
            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetAdvertsFromCategory(string catText)
        {
            int catId = Int32.Parse(
                db.Advert_Category.Select(x => new SelectListItem() { 
                    Text = x.Name, Value = x.ACCode.ToString() 
                }).ToList().FirstOrDefault(x => x.Text == catText).Value);

            ViewData["alphabetic"] = new string[] { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            ViewData["category"] = db.Advert_Category.Select(x => new SelectListItem() { Text = x.Name, Value = x.ACCode.ToString() }).ToList(); 
            ViewData["adverts"] = db.Advert.Where(x => x.ACCode == catId).ToList();
            
            return View();
        }

        public ActionResult GetAdvertsByLetter(string letter)
        {
            ViewData["alphabetic"] = new string[] { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            ViewData["category"] = db.Advert_Category.Select(x => new SelectListItem() { Text = x.Name, Value = x.ACCode.ToString() }).ToList();

            var lettersID = db.Advert.Select(x => new { Letter = x.Title.Substring(0, 1).ToUpper(), ID = x.ACode }).ToList();
            letter = letter.ToUpper();

            var IDs = lettersID.Where(x => x.Letter == letter ).Select(x => x.ID).ToList();

            ViewData["adverts"] = db.Advert.Where(x => IDs.Contains(x.ACode)).ToList();
                //(from adv in db.Advert
                //                   where adv.Title[0].ToString().ToUpper() == letter
                //                   select adv).ToList();
                //db.Advert.Where(x => x.Title).ToList();
            return View("GetAdvertsFromCategory");
        }

        public ActionResult GetAdvertsBySearch(string search)
        {
            ViewData["alphabetic"] = new string[] { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            ViewData["category"] = db.Advert_Category.Select(x => new SelectListItem() { Text = x.Name, Value = x.ACCode.ToString() }).ToList();

            ViewData["adverts"] = db.Advert.Where(x => x.Title.ToUpper().Contains(search.ToUpper())).ToList();

            return View("GetAdvertsFromCategory");
        }

        public ActionResult GetAdvertByID(int id)
        {
            ViewData["alphabetic"] = new string[] { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            ViewData["category"] = db.Advert_Category.Select(x => new SelectListItem() { Text = x.Name, Value = x.ACCode.ToString() }).ToList();

            Advert adv = db.Advert.FirstOrDefault(x => x.ACode == id);
            return View("GetAdvert",adv);
        }

        public ActionResult AllowAdvert(int code)
        {
            ViewData["alphabetic"] = new string[] { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            ViewData["category"] = db.Advert_Category.Select(x => new SelectListItem() { Text = x.Name, Value = x.ACCode.ToString() }).ToList();

            Advert adv = db.Advert.FirstOrDefault(x => x.ACode == code);
            adv.Status = (int)Constants.AdvertStatus.Allowed;
            db.SaveChanges();
            return View("GetAdvert", adv);
        }

        public ActionResult ForbidAdvert(int code)
        {
            ViewData["alphabetic"] = new string[] { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            ViewData["category"] = db.Advert_Category.Select(x => new SelectListItem() { Text = x.Name, Value = x.ACCode.ToString() }).ToList();

            Advert adv = db.Advert.FirstOrDefault(x => x.ACode == code);
            adv.Status = (int)Constants.AdvertStatus.NotAllowed;
            db.SaveChanges();
            return View("GetAdvert", adv);
        }
    }
}
