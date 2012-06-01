using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskSystem.Data.Context;
using TaskSystem.Models;

namespace TaskSystem.Controllers
{
    public class TaskController : Controller
    {
        private TaskContext _context = new TaskContext("TaskWebsite");
        //
        // GET: /Task/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Task/Details/5

        [Authorize]
        public ActionResult Details(int id)
        {
            var user = Membership.GetUser(true);

            var taskType = new UserTaskType() { Name = "Task type 1" };
            _context.Save(taskType);
            _context.SaveChanges();

            var task = new UserTask()
                           {
                               Name = "test",
                               UserId = (Guid)user.ProviderUserKey,
                               UserTaskType = taskType
                           };
            _context.Save(task);
            _context.SaveChanges();

            return View(_context.Tasks.ToList());
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Task/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Task/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Task/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
