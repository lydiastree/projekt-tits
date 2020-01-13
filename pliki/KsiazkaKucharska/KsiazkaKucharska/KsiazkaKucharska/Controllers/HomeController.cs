using KsiazkaKucharska.context;
using KsiazkaKucharska.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KsiazkaKucharska.Controllers
{
    public class HomeController : Controller
    {
        private KsiazkaContext db = new KsiazkaContext();
        public ActionResult Index()
        {
            var listadan = db.Dania.ToList();
            var listazup = db.Zupy.ToList();
            var listaprzepisow = db.Przepisy.ToList();

            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}