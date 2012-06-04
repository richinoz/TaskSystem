using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskSystem.Data.Context;
using TaskSystem.Helpers;
using TaskSystem.Models;

namespace TaskSystem.Controllers
{

    [AuthorizeTaskSystem]
    public class TaskController : Controller
    {
        private readonly ITaskContext _context;

        //
        // GET: /Task/

        public TaskController(ITaskContext taskContext)
        {
            _context = taskContext;
        }

        public ActionResult Details(string sortColumn)
        {
            var user = Membership.GetUser(true);

            var model = _context.Tasks.Where(x => x.UserId == (Guid)user.ProviderUserKey);
            bool sortAsc = true;

            if (!string.IsNullOrWhiteSpace(sortColumn))
            {
                if (TempData[sortColumn] == null)
                {
                    TempData[sortColumn] = true;
                }
                sortAsc = (bool)TempData[sortColumn];
                TempData[sortColumn] = !sortAsc;


                switch (sortColumn)
                {

                    case "date":
                        model = model.OrderBy(x => x.DueDate);
                        break;

                    case "priority":
                        model = model.OrderBy(x => x.TaskPriority);
                        break;

                    case "description":
                        model = model.OrderBy(x => x.Description);
                        break;

                }

            }

            var list = model.ToList();
            if (!sortAsc)
                list.Reverse();

            ViewBag.UserName = user.UserName;

            if (Request.IsAjaxRequest())
                return PartialView(list);

            return View(list);
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            var user = Membership.GetUser(true);

            var task = new UserTask()
            {

                UserId = (Guid)user.ProviderUserKey,
                TaskPriority = 1,
                DueDate = DateTime.Now.Date

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

                return new UpdateActionResult(RedirectToAction("Details"), View(), () =>
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
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);
            return View(task);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(UserTask userTask)
        {
            try
            {

                return new UpdateActionResult(RedirectToAction("Details"), View(), () =>
                            {
                                _context.Entry(userTask).State = EntityState.Modified;
                                _context.SaveChanges();
                            });

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
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);
            return View(task);
        }

        //
        // POST: /Task/Delete/5

        [HttpPost]
        public ActionResult Delete(UserTask usertask)
        {
            try
            {
                var task = _context.Tasks.FirstOrDefault(x => x.Id == usertask.Id);

                return new UpdateActionResult(RedirectToAction("Details"), View(), () =>
                                    {
                                        _context.Remove(task);
                                        _context.SaveChanges();
                                    });
            }
            catch
            {
                return (View());
            }
        }
    }
}
