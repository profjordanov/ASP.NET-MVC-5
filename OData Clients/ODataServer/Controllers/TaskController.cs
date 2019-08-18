using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ODataServer.Models;

namespace ODataServer.Controllers
{   
    public class TaskController : Controller
    {
		private readonly ITaskRepository taskRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public TaskController() : this(new TaskRepository())
        {
        }

        public TaskController(ITaskRepository taskRepository)
        {
			this.taskRepository = taskRepository;
        }

        //
        // GET: /Task/

        public ViewResult Index()
        {
            return View(taskRepository.AllIncluding((t)=>t.AssignedTo));
        }

        //
        // GET: /Task/Details/5

        public ViewResult Details(int id)
        {
            return View(taskRepository.Find(id));
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
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid) {
                taskRepository.InsertOrUpdate(task);
                taskRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Task/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(taskRepository.Find(id));
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid) {
                taskRepository.InsertOrUpdate(task);
                taskRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Task/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(taskRepository.Find(id));
        }

        //
        // POST: /Task/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            taskRepository.Delete(id);
            taskRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

