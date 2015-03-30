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
        //
        // GET: /Home/

        private SmekayEntities db = new SmekayEntities();

        public ActionResult Index()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            int i = 0;
            foreach(Advert_Category ac in db.Advert_Category.ToList())
            {
                li.Add(new SelectListItem { Text = ac.Name, Value = i.ToString() });
                i++;
            }

            ViewData["category"] = li;
            ViewData["cities"] = getCitiesByCountry(1);
            ViewData["adverts"] = getAdverts().Take(7).ToList();
            ViewData["categoryAdverts"] = getCategorysCovers();

            return View();
        }

        private List<City> getCitiesByCountry(int countryCode)
        {
            return db.City.Where(x => x.CoCode == countryCode).ToList();
        }

        private List<Advert> getAdverts()
        {
            return db.Advert.Select(x => x).OrderBy(x => x.Date).ToList();
        }

        private int getAdvertsCountInCategory(Advert_Category category)
        {
            return db.Advert.Where(x => x.ACCode == category.ACCode).ToList().Count();
        }

        private List<CategoryCover> getCategorysCovers()
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
    }
}
