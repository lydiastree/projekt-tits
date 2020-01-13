using KsiazkaKucharska.context;
using KsiazkaKucharska.Models;
using KsiazkaKucharska.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Net;
using System.Data.Entity;

namespace KsiazkaKucharska.Controllers
{
    public class PrzepisyController : Controller
    {
        private KsiazkaContext db = new KsiazkaContext();

        public ActionResult Index()
        {
            var przepisy = db.Przepisy.ToList();
            var vm = new ViewModels()
            { Przepisy = przepisy };
            return View(vm);
        }


        public ActionResult Create(Przepis przepis)
        {
            string login = User.Identity.GetUserName();
            if(login == "")
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    przepis.Ktododal = login;
                    db.Przepisy.Add(przepis);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(przepis);
            }

           
        }
        public ActionResult AddCom(int id)
        {
            Przepis przepis = db.Przepisy.Find(id);
            var komentarze = db.Komentarze.ToList();
            var vm = new ViewModels()
            { Komentarze = komentarze.Where(x => x.PrzepisID == przepis.PrzepisID) };
            return View(vm);
        }

        public ActionResult Com(Comment comment, int id)
        {
            string login = User.Identity.GetUserName();
            comment.Nazwauzyt = login;
            if (ModelState.IsValid)
            {
                comment.PrzepisID = id;
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