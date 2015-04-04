using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smekay24.Controllers
{
    public class AdvertController : Controller
    {
        //
        // GET: /Advert/

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
            return View();
        }

    }
}
