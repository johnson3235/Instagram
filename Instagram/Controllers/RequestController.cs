using Instagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Instagram.ViewModels;
using System.Data.Entity.Validation;
using Newtonsoft.Json;

namespace Instagram.Controllers
{
    public class RequestController : Controller
    {
        private InstagramEntities db = new InstagramEntities();
        // GET: Request
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Accept(int user_id, int request_id)
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                int accept = 2 ;
                var users = db.user_requests.Where(x => x.user_id == user_id).Where(x => x.request_id == request_id).SingleOrDefault();
                user_requests request = new user_requests
                {
                    user_id = request_id,
                    request_id = user_id,
                    status = accept
                };
                users.status = accept;
                db.Entry(users).State = EntityState.Modified;
                db.user_requests.Add(request);
                db.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());

            }
        }

        [HttpGet]
        public ActionResult UserRequests()
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
               int user_id = Convert.ToInt32(Session["user_id"]); 
                List<user_requests> users = db.user_requests.Where(x => x.user_id == user_id).Where(x => x.status == 0).ToList();
                List<user> useres = db.users.ToList();
                var user = db.users.SingleOrDefault(x => x.id == user_id);
                UserRequests userRequest = new UserRequests
                {

                    user_page = user,
                    friend1= users,
                    user_list=useres

                };

                return View(userRequest);

            }
        }


        [HttpPost]
        public ActionResult SendRequest(int user_id, int request_id)
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {

                int status = 0;
                user_requests request = new user_requests
                {
                    user_id = user_id,
                    request_id = request_id,
                    status = status
                };
                db.user_requests.Add(request);
                db.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
        }

        [HttpPost]
        public ActionResult Reject(int user_id, int request_id)
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {

                int reject = 1;
                var users = db.user_requests.Where(x => x.user_id == user_id).Where(x => x.request_id == request_id).SingleOrDefault();
                user_requests request = new user_requests
                {
                    user_id = request_id,
                    request_id = user_id,
                    status = reject
                };
                users.status = reject;
                db.Entry(users).State = EntityState.Modified;
                db.user_requests.Add(request);
                db.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            
        }


        [HttpPost]
        public ActionResult Cancel(int user_id, int request_id)
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                var user1 = db.user_requests.Where(x => x.user_id == request_id).Where(x => x.request_id == user_id).SingleOrDefault();
                var user2 = db.user_requests.Where(x => x.user_id == user_id).Where(x => x.request_id == request_id).SingleOrDefault();
                db.user_requests.Remove(user1);
                db.user_requests.Remove(user2);
                db.SaveChanges();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            
        }
    }
}