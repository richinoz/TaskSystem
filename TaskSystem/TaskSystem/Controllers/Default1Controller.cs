using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskSystem.Models;
using TaskSystem.Data.Context;

namespace TaskSystem.Controllers
{ 
    public class Default1Controller : Controller
    {
        private TestContext db = new TestContext();

        //
        // GET: /Default1/

        public ViewResult Index()
        {
            return View(db.UserTasks.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ViewResult Details(int id)
        {
            UserTask usertask = db.UserTasks.Find(id);
            return View(usertask);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(UserTask usertask)
        {
            if (ModelState.IsValid)
            {
                db.UserTasks.Add(usertask);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(usertask);
        }
        
        //
        // GET: /Default1/Edit/5
 
        public ActionResult Edit(int id)
        {
            UserTask usertask = db.UserTasks.Find(id);
            return View(usertask);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(UserTask usertask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usertask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usertask);
        }

        //
        // GET: /Default1/Delete/5
 
        public ActionResult Delete(int id)
        {
            UserTask usertask = db.UserTasks.Find(id);
            return View(usertask);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            UserTask usertask = db.UserTasks.Find(id);
            db.UserTasks.Remove(usertask);
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