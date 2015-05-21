using Smekay24.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smekay24.Controllers
{
    public class HomeController : Controller
    {

        private SmekayEntities db = new SmekayEntities();

        public ActionResult Index()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            int i = 0;
            foreach(Advert_Category ac in db.Advert_Category.ToList())
                categories.Add(new SelectListItem { Text = ac.Name, Value = ac.ACCode.ToString() });

            ViewData["category"] = categories;
            ViewData["cities"] = getCitiesByCountry(1);
            ViewData["adverts"] = getAdverts().Take(7).ToList();
            ViewData["categoryAdverts"] = getCategoriesCovers();
            ViewData["allAsvertCount"] = getAllAdvertCount();

            return View();
        }

        private List<City> getCitiesByCountry(int countryCode)
        {
            return db.City.Where(x => x.CoCode == countryCode).ToList();
        }

        private List<Advert> getAdverts()
        {
            var items = db.Advert.Where(x => x.Status == (int)Constants.AdvertStatus.Allowed).OrderByDescending(x => x.Date).ToList();

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

            return items;
        }

        private int getAdvertsCountInCategory(Advert_Category category)
        {
            return db.Advert.Where(x => x.ACCode == category.ACCode).ToList().Count();
        }

        private List<CategoryCover> getCategoriesCovers()
        {
            List<CategoryCover> list = new List<CategoryCover>();

            foreach (Advert_Category cat in db.Advert_Category)
            {
                list.Add(new CategoryCover() { 
                    ACCode = cat.ACCode,
                    CountAdvert = getAdvertsCountInCategory(cat),
                    Desc = cat.Desc,
                    Name = cat.Name
                });
            }

            return list;
        }

        private int getAllAdvertCount()
        {
            return db.Advert.Count();
        }
    }
}
