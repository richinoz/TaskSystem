﻿using System;
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
                bool sortAsc = true;

                if (TempData[sortColumn] == null)
                {
                    TempData[sortColumn] = true;
                }
                sortAsc = (bool)TempData[sortColumn];
                TempData[sortColumn] = !sortAsc;

                if(sortAsc)
                {
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
                else
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

            }

            ViewBag.UserName = user.UserName;

            return View(model);
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            var user = Membership.GetUser(true);

            var task = new UserTask()
            {
                Description = "test",
                UserId = (Guid)user.ProviderUserKey,
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
            var task = _context.Tasks.FirstOrDefault(x=>x.Id==id);
            return View(task);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(UserTask userTask)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return new UpdateActionResult(RedirectToAction("Details"), () =>
                                {
                                    _context.Entry(userTask).State = EntityState.Modified;                                                                                       
                                    _context.SaveChanges();
                                });
                }
                return View();
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
            var task = _context.Tasks.FirstOrDefault(x=>x.Id ==id);
            return View(task);
        }

        //
        // POST: /Task/Delete/5

        [HttpPost]
        public ActionResult Delete(UserTask usertask)
        {
            try
            {
                var task = _context.Tasks.FirstOrDefault(x=>x.Id==usertask.Id);

                return new UpdateActionResult(RedirectToAction("Details"), ()=>
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
