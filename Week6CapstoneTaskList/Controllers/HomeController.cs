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
            if (ORM.Tasks.ToList().Count == 0)
            {
                newTask.taskNumber = 1;
            }
            else
            {
                newTask.taskNumber = ((ORM.Tasks.ToList().Last().taskNumber) + 1);
            }
            ORM.Tasks.Add(newTask);
            ORM.SaveChanges();
            return RedirectToAction("Welcome");
        }
        public ActionResult DeleteTask()
        {
            return View();
        }
        public ActionResult TaskList()
        {
            return View();
        }
    }
}