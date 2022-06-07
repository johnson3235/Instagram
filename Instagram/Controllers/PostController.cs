using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Instagram.Models;
using System.Data.Entity;
using Instagram.ViewModels;
using System.Data.Entity.Validation;
using Newtonsoft.Json;
namespace Instagram.Controllers
{
    public class PostController : Controller
    {

        private InstagramEntities db = new InstagramEntities();


        [HttpGet]
        [Route("Post/Index")]
        public ActionResult Index()
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                var id = Convert.ToInt32(Session["user_id"]);
                var mainuser = db.users.SingleOrDefault(x => x.id == id);
                var getuser_request2 = db.user_requests.ToList();
                List<post> postes = db.posts.Where(x => x.user_id != id).OrderByDescending(r => r.created_at).ToList();
                List<user> useres = db.users.ToList();
                List<comment_posts> comments_post = db.comment_posts.ToList();
                List<like> Likess = db.likes.ToList();
                List<user_requests> Requests = db.user_requests.Where(x => x.user_id == id).Where(x=>x.status == 0).Take(1).ToList();
                PostUser user_post = new PostUser
                {
                    Mainuser = mainuser,
                    user = useres,
                    post = postes,
                    comments = comments_post,
                    likes = Likess,
                    Request = Requests,
                    friend1 = getuser_request2
                };

                return View(user_post);

            }
        }


        [HttpGet]
        [Route("Post/Likes/{id}")]
        public ActionResult Likes(int id)
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                var ids = Convert.ToInt32(Session["user_id"]);
                int post_id = id;
                var users = db.users.SingleOrDefault(x => x.id == ids);
                var posts = db.posts.SingleOrDefault(x => x.id == post_id);
                var likes = db.likes.Where(x => x.post_id == post_id).Where(y => y.user_id == ids).SingleOrDefault();
                like likePost = new like();
                if (likes == null)
                {

                    likePost.status = 1;
                    likePost.user_id = ids;
                    likePost.post_id = post_id;
                    posts.likes += 1;
                }
                else
                {
                    likePost.post_id = post_id;
                    likePost.user_id = ids;
                    if (likes.status == 0)
                    {
                        likePost.status = 1;
                        posts.likes += 1;
                    }
                    else
                    {
                        likePost.status = 0;
                        posts.likes -= 1;
                    }
                }
                UpdateLike user = new UpdateLike
                {
                    user = users,
                    post = posts,
                    likes = likePost
                };
                if (likes == null)
                {
                    db.likes.Add(user.likes);
                }
                else
                {
                    likes.status = user.likes.status;
                    db.Entry(likes).State = EntityState.Modified;
                }

                db.Entry(posts).State = EntityState.Modified;
                db.SaveChanges();

                return Redirect(Request.Headers["Referer"].ToString());
            }
            
        }


        [HttpGet]
        public ActionResult Postdetails(int post_id, int user_id, int? user_page)
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                var mainuser = db.users.SingleOrDefault(x => x.id == user_id);
                user user_pages;
                var getuser_request = db.user_requests.ToList();
                if (user_page != null)
                {
                    user_pages = db.users.SingleOrDefault(x => x.id == user_page);
                }
                else
                {
                    user_pages = db.users.SingleOrDefault(x => x.id == user_id);
                }
                List<like> Likess = db.likes.ToList();
                post post = db.posts.Where(x => x.id == post_id).SingleOrDefault();
                List<user> useres = db.users.ToList();
                user user_post = db.users.Where(x => x.id == post.user_id).SingleOrDefault();
                List<comment_posts> comments_post = db.comment_posts.Where(x => x.post_id == post_id).ToList();
                var users = db.users.ToList();
                var posts = db.posts.ToList();
                PostsMain post_user = new PostsMain
                {
                    Mainuser = mainuser,
                    user_page = user_pages,
                    comments = comments_post,
                    users = users,
                    likes = Likess,
                    user_post = user_post,
                    post = post,
                    friend1 = getuser_request
                };
                return View(post_user);
            }
        }
        



        [HttpPost]
        public ActionResult Create(int user_id, int post_id, string comment)
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                user_id = Convert.ToInt32(Session["user_id"]);
                comment_posts comments = new comment_posts
                {
                    user_id = user_id,
                    post_id = post_id,
                    comment = comment,
                    created_at = DateTime.Now

                };

                if (ModelState.IsValid)
                {

                    db.Entry(comments).State = EntityState.Modified;
                    db.comment_posts.Add(comments);
                    db.SaveChanges();
                }
                return Redirect(Request.Headers["Referer"].ToString());

            }
        }



        [HttpGet]
        public ActionResult CreatePost()
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                var id = Convert.ToInt32(Session["user_id"]);
                user mainuser = db.users.SingleOrDefault(x => x.id == id);
                return View(mainuser);
            }
        }



        [HttpPost]
        [Route("Post/CreatePostes")]

        public ActionResult CreatePostes(HttpPostedFileBase image, int user_id, string comment, int likes)
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                return RedirectToRoute("Login");
            }
            else
            {
                post poster = new post();
                if (ModelState.IsValid)
                {
                    if (image != null)
                    {
                        string pic = System.IO.Path.GetFileName(image.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/images/"), pic);

                        image.SaveAs(path);

                        poster.images = pic;
                        poster.comment = comment;
                        poster.user_id = user_id;
                        poster.likes = 0;
                        poster.created_at = DateTime.Now;
                        db.posts.Add(poster);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
        }
        
    }
}





