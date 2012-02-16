using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Controllers
{ 
    public class HomeController : Controller
    {
        private NHLdb db = new NHLdb();

        //
        // GET: /

        public ViewResult Index()
        {
            return View(db.Users.ToList());
        }

        //
        // GET: /Details/5

        public ViewResult Details(int id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // GET: /Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(user);
        }
        
        //
        // GET: /Edit/5
 
        public ActionResult Edit(int id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Delete/5
 
        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}