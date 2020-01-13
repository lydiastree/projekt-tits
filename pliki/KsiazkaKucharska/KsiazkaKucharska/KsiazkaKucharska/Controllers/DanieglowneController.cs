using KsiazkaKucharska.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KsiazkaKucharska.context;
using KsiazkaKucharska.ViewModel;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;


namespace KsiazkaKucharska.Controllers
{
    public class DanieglowneController : Controller
    {
        private KsiazkaContext db = new KsiazkaContext();
        
        public ActionResult Index()
        {
            var dania = db.Dania.ToList();
            var vm = new ViewModels()
            { Daniaglowne = dania };
            return View(vm);
        }

        public ActionResult AddCom(int id)
        {
            Danieglowne danieglowne = db.Dania.Find(id);
            var komentarze = db.Komentarze.ToList();
            var vm = new ViewModels()
            { Komentarze = komentarze.Where(x => x.DanieglowneID == danieglowne.DanieglowneID) };
            return View(vm);
        }

        public ActionResult Com(Comment comment, int id)
        {
            string login = User.Identity.GetUserName();
            comment.Nazwauzyt = login;
            if (ModelState.IsValid)
            {
                comment.DanieglowneID = id;
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

        public ActionResult Create(Danieglowne danieglowne)
        {
            string login = User.Identity.GetUserName();
            if (login == "")
            {
                ViewBag.Error = "Aby dodać przepis należy się zalogować!";
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    danieglowne.Ktododal = login;
                    db.Dania.Add(danieglowne);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(danieglowne);
            }
        }
    }
}