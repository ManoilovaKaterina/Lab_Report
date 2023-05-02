using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ookplab5.Models;

namespace ookplab5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string result = "")
        {
            ViewData["Result"] = result;
            ViewData["Question"] = "Лев, Михайло і Роман працюють в банку як бухгалтер, касир і начальник відділу. " +
                "Якщо Роман – касир, то Михайло – начальник відділу. Якщо Роман – начальник відділу, то Михайло – бухгалтер. " +
                "Якщо Михайло – не касир, то Лев – не начальник відділу. Якщо Лев – бухгалтер, то Роман – начальник відділу. " +
                "Хто яку посаду обіймає в банку?";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string n1, string p1, string n2, string p2, string n3, string p3)
        {
            Logic L = new Logic();
            Person per1 = new Person(n1, p1);
            Person per2 = new Person(n2, p2);
            Person per3 = new Person(n3, p3);
            string res = L.GetResults(per1, per2, per3);
            return RedirectToAction("Index", new { result = res });
        }
    }
}