using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Instagram.Models;
using System.Data.Entity;
using Instagram.ViewModels;
using System.Collections;

namespace Instagram.Controllers
{
    public class UserController : Controller
    {
        // GET: User
      

        private InstagramEntities db = new InstagramEntities();

        [HttpGet]
        [Route("Home/user")]
        public ActionResult Indexs()
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                List<user> users = db.users.ToList();
                return View(users);
            }
        }



    



        [HttpGet]
       // [Route("Home/user/profile")]
        public ActionResult Profile()
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                return View();

            }
        }


        [HttpGet]
        // [Route("user/FriendList")]
        public ActionResult FriendList(int? user_id)
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                int id = Convert.ToInt32(Session["user_id"]);
                user Mainuser;
                user user_page;
                List<user_requests> getuser_request;
                List<user_requests> getuser_request2;
                List<user> userss;
                Mainuser = db.users.SingleOrDefault(x => x.id == id);
                userss = db.users.ToList();
                if (user_id != null)
                {
                    user_page = db.users.SingleOrDefault(x => x.id == user_id);
                    getuser_request = db.user_requests.ToList();
                }
                else
                {
                    user_page = db.users.SingleOrDefault(x => x.id == id);
                    getuser_request = db.user_requests.ToList();
                }
                FriendUser users = new FriendUser
                {
                    Mainuser = Mainuser,
                    user_page = user_page,
                    user = userss,
                    friend1 = getuser_request
                };

                return View(users);
            }
        }






        [HttpGet]

        public ActionResult UserDetails(int user_id)
        {

            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                int id = Convert.ToInt32(Session["user_id"]);
                var user = db.users.SingleOrDefault(x => x.id == id);
                var user_page = db.users.SingleOrDefault(x => x.id == user_id);
                var userss = db.users.ToList();
                var getuser_request = db.user_requests.ToList();
                var getuser_request2 = db.user_requests.ToList();
                FriendUser users = new FriendUser
                {
                    Mainuser = user,
                    user_page = user_page,
                    user = userss,
                    friend1 = getuser_request,

                };

                return View(users);
            }
        }
        



        [HttpGet]
        // [Route("user/Posts")]
        public ActionResult UserPosts(int? user_id)
        {

            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                int id = Convert.ToInt32(Session["user_id"]);
                user Mainuser;
                user user_page;
                List<user> useres;
                List<post> postes;
                List<comment_posts> comments_post;
                List<like> Likess;
                Mainuser = db.users.SingleOrDefault(x => x.id == id);
                useres = db.users.ToList();
                comments_post = db.comment_posts.ToList();
                Likess = db.likes.ToList();
                var getuser_request = db.user_requests.ToList();
                if (user_id != null)
                {

                    user_page = db.users.SingleOrDefault(x => x.id == user_id);
                    postes = db.posts.Where(y => y.user_id == user_id).OrderByDescending(r => r.created_at).ToList();

                }
                else
                {

                    user_page = db.users.SingleOrDefault(x => x.id == id);
                    postes = db.posts.Where(y => y.user_id == id).OrderByDescending(r => r.created_at).ToList();

                }

                PostUser user_post = new PostUser
                {
                    Mainuser = Mainuser,
                    user_page = user_page,
                    user = useres,
                    post = postes,
                    comments = comments_post,
                    likes = Likess,
                    friend1 = getuser_request
                };

                return View(user_post);
            }
            
        }



        
        [HttpGet]
        public ActionResult UserEdit()
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                int id = Convert.ToInt32(Session["user_id"]);

                if (id != null)
                {
                    var user = db.users.SingleOrDefault(a => a.id == id);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }


                    return View(user);
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
        }






        [HttpPost]
        public ActionResult UserEdit2(user acounnt, HttpPostedFileBase file)
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                var user = db.users.SingleOrDefault(a => a.id == acounnt.id);
                user.fname = acounnt.fname;
                user.lname = acounnt.lname;
                user.phone = acounnt.phone;
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/"), pic);
                    file.SaveAs(path);

                    user.images = pic;
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                Session["User"] = user;
                return RedirectToAction("UserPosts", new { id = acounnt.id });

            }
        }

        [HttpPost]
        public ActionResult UserSearch(string Searching)
        {

            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                int id = Convert.ToInt32(Session["user_id"]);
                List<user> user;
                if (!string.IsNullOrEmpty(Searching))
                {
                    user = db.users.Where(x => x.fname == Searching || x.lname == Searching || x.email == Searching).Where(x => x.id != id).ToList();
                    return View(user);
                }
                else
                {
                    return RedirectToRoute("Home");
                }
            }

        }

    }
}

