using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskSystem.Data.Context;
using TaskSystem.Helpers;
using TaskSystem.Models;

namespace TaskSystem.Controllers
{
    [Authorize]
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


        public ActionResult Details(string sortColumn)
        {
            var user = Membership.GetUser(true);

            var model = _context.Tasks.Where(x => x.UserId == (Guid)user.ProviderUserKey);

            if (!string.IsNullOrWhiteSpace(sortColumn))
            {
                switch (sortColumn)
                {
                    case "date":
                        model = model.OrderByDescending(x => x.DueDate);
                        break;

                    case "priority":
                        model = model.OrderByDescending(x => x.TaskPriority);
                        break;

                    case "description":
                        model = model.OrderByDescending(x => x.Description);
                        break;

                }

            }

            ViewBag.UserName = user.UserName;

            return View(model);
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            var user = Membership.GetUser(true);

            //var taskType = _context.TaskTypes.First();

            var task = new UserTask()
            {
                Description = "test",
                UserId = (Guid)user.ProviderUserKey,
                // UserTaskType = taskType,
                TaskPriority = 1,
                DueDate = DateTime.Now.Date.SqlValidDateTime()

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
                
                return new UpdateActionResult(RedirectToAction("Details"), () =>
                                                  {
                                                      _context.Save(userTask);
                                                      _context.SaveChanges();                                                      
                                                  });

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
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
                return new UpdateActionResult(RedirectToAction("Index"), null);
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

                return new UpdateActionResult(RedirectToAction("Index"), null);
            }
            catch
            {
                return (View());
            }
        }
    }
}
