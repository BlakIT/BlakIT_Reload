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
                    img.SaveAs(Server.MapPath("~/Files/" + fileName));
                }
            }

            db.Advert.Add(advert);
            db.SaveChanges();
            
            return RedirectToAction("Index", "Home");
        }

    }
}
