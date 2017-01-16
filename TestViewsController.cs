using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projectForTestingStuff.Controllers.DataControllers;
using projectForTestingStuff.Models;

namespace projectForTestingStuff.Controllers
{
    public class TestViewsController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            //http post works and masks the email and password and it helps to keep the app fast
            //this method keeps the form looking clean but the email and pass are being successfully passed into the controller!

            UserDataController udc = new UserDataController("DefaultConnection");

            List<UserModel> thingy = udc.checkUser(email, password);

            return View(thingy);
        }
    }
}