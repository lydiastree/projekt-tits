using KsiazkaKucharska.context;
using KsiazkaKucharska.Models;
using KsiazkaKucharska.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KsiazkaKucharska.Controllers
{
    public class ZupyController : Controller
    {
        private KsiazkaContext db = new KsiazkaContext();

        public ActionResult Index()
        {
            var zupy = db.Zupy.ToList();
            var vm = new ViewModels()
            { Zupy = zupy };
            return View(vm);
        }

        public ActionResult Create(Zupa zupa)
        {
            string login = User.Identity.GetUserName();
            if (login == "")
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    zupa.Ktododal = login;
                    db.Zupy.Add(zupa);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(zupa);
            }
        }
        public ActionResult AddCom(int id)
        {
            Zupa zupa = db.Zupy.Find(id);
            var komentarze = db.Komentarze.ToList();
            var vm = new ViewModels()
            { Komentarze = komentarze.Where(x => x.ZupaID == zupa.ZupaID) };
            return View(vm);
        }

        public ActionResult Com(Comment comment, int id)
        {
            string login = User.Identity.GetUserName();
            comment.Nazwauzyt = login;
            if (ModelState.IsValid)
            {
                comment.ZupaID = id;
                var data = DateTime.Now;
                comment.Datadodania = data;
                db.Komentarze.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        public ActionResult DelCom(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment komentarz = db.Komentarze.Find(id);
            if (komentarz == null)
            {
                return HttpNotFound();
            }
            return View(komentarz);
        }
        [HttpPost]
        public ActionResult DelCom(int id, FormCollection collection)
        {
            Comment komentarz = db.Komentarze.Find(id);
            db.Komentarze.Remove(komentarz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}