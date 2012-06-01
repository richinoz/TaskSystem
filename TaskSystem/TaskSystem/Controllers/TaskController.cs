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
        private readonly ITaskContext _context;

        //
        // GET: /Task/

        public TaskController(ITaskContext taskContext)
        {
            _context = taskContext;
        }

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

            var model = _context.Tasks.Where(x => x.UserId == (Guid)user.ProviderUserKey);

            ViewBag.UserName = user.UserName;

            return View(model);
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            var user = Membership.GetUser(true);

            var taskType = _context.TaskTypes.First();

            var task = new UserTask()
            {
                Name = "test",
                UserId = (Guid)user.ProviderUserKey,
                UserTaskType = taskType,
                TaskPriority = 1,
                DueDate = DateTime.MinValue.SqlValidDateTime()

            };

            return View(task);
        }

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(UserTask userTask)
        {
            try
            {
                // TODO: Add insert logic here

                var user = Membership.GetUser(true);

                userTask.UserId = (Guid)user.ProviderUserKey;

                _context.Save(userTask);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
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
