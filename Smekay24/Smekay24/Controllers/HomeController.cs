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
            ViewData["adverts"] = getAdverts();

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
    }
}
