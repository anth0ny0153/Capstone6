using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week6CapstoneTaskList.Models;

namespace Week6CapstoneTaskList.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginToAccount(string email, string password)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();

            User Account = ORM.Users.Find(email);
            if(Account == null)
            {
                ViewBag.Error = "User does not exist.";
                return View("Index");
            }
            else if (Account.password != password)
            {
                ViewBag.Error = "Incorrect password.";
                return View("Index");
            }
            else if (Account.email == email && Account.password == password)
            {
                ViewBag.Message = "Welcome!";
                return View("Welcome");
            }
            else
            {
                return View("Index");
            }
        }
        public ActionResult Welcome()
        {
            return View();
        }
        public ActionResult SignUpForm()
        {
            return View();
        }
        public ActionResult CreateAccount(User newUser)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
            ORM.Users.Add(newUser);
            ORM.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Task()
        {
            return View();
        }
        public ActionResult AddNewTask(Task newTask)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
                ORM.Tasks.Add(newTask);
                ORM.SaveChanges();
                return RedirectToAction("TaskList");
        }
        public ActionResult DeleteTask(string taskName)
        {
            //1. ORM 

            TaskListDBEntities ORM = new TaskListDBEntities();

            //2. Find the customer you want to delete 
            Task Found = ORM.Tasks.Find(taskName);



            if (Found != null)
            {
                ORM.Tasks.Remove(Found);

                ORM.SaveChanges();

                return RedirectToAction("TaskList");

            }
            else
            {
                ViewBag.ErrorMessage = "Task not found!";
                return View("Error");

            }
        }
        public ActionResult TaskList()
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
            ViewBag.TaskList = ORM.Tasks.ToList();
            return View();

        }
        public ActionResult ChangeStatus(string taskName)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
            Task Found = ORM.Tasks.Find(taskName);
            if(Found != null)
            {
                if(Found.taskStatus == "Incomplete")
                {
                    Found.taskStatus = "Complete";
                }
                else
                {
                    Found.taskStatus = "Incomplete";
                }
                ORM.Entry(Found).State = System.Data.Entity.EntityState.Modified;
                ORM.SaveChanges();
                return RedirectToAction("TaskList");
            }
            else
            {
                return View("Error");
            }
        }
        public JsonResult SearchByName(string taskName)
        {
            //orm
            TaskListDBEntities ORM = new TaskListDBEntities();
            //search by country
            List<Task> Result = ORM.Tasks.Where(c => c.taskName.Contains(taskName)).ToList();
            //return data as json
            return Json(Result);
        }
    }
}