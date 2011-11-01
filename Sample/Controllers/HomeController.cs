using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoBox.Sample.Repositories.Abstraction;

namespace AutoBox.Sample.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ITestRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            ViewBag.TimeStamp = repository.GetTimeStamp().Now;

            return View();
        }

        public ActionResult UpdateTimeStamp()
        {
            repository.UpdateTimeStamp();
            return Redirect("/");
        }

        public ActionResult About()
        {
            return View();
        }

        private ITestRepository repository;
    }
}
