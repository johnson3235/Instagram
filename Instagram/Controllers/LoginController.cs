using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Instagram.Models;
using Instagram.Controllers;
namespace Instagram.Controllers
{
    public class LoginController : Controller
    {
        private InstagramEntities db = new InstagramEntities();
        [HttpGet]
       [Route("Login/Index")]
        public ActionResult Index()
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return View();
            }
            else
            {
               return RedirectToRoute("Home");
            }

        }

        [HttpPost]
        [Route("Login/Verify")]
        public ActionResult Verify(user acc)
        {
            var login = db.users.Where(x => x.email == acc.email).Where(x => x.password == acc.password).SingleOrDefault();

            if (login != null)
            {
                Session["user_id"] = login.id;
                int x = Convert.ToInt32(Session["user_id"]);
                user userinfo = new user();
                userinfo = db.users.Find(x);
                Session["User"] = userinfo;
                return RedirectToRoute("Home");

            }
            else
            {

                ViewBag.Notification = "Data Doesn't Match Try Again";
                return View("Index");
            }

        }



        [HttpGet]
        [Route("Login/Logout")]
        public ActionResult Logout()
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
            }
            else
            {
                Session.Contents.RemoveAll();
            }
            return RedirectToRoute("Login");

        }
    }
}