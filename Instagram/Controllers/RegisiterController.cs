using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Instagram.Models;
namespace Instagram.Controllers
{
    public class RegisiterController : Controller
    {

        public Boolean IsEmailAddressExist(string email)
        {
            var validateName = db.users.FirstOrDefault(x => x.email == email);
            if (validateName != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private InstagramEntities db = new InstagramEntities();
        [HttpGet]
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
        public ActionResult Regisiter(user user, HttpPostedFileBase file)
        {
            if (Session["user_id"] == "0" || Session["user_id"] == null)
            {
                if(IsEmailAddressExist(user.email) == true)
                {
                    if (ModelState.IsValid)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            string filename = System.IO.Path.GetFileName(file.FileName);
                            string imgpath = System.IO.Path.Combine(Server.MapPath("~/images/"), filename);
                            file.SaveAs(imgpath);
                            user.images = filename;
                        }
                        else
                        {
                            user.images = "default.png";
                        }


                        user acounnt = new user
                        {
                            fname = user.fname,
                            lname = user.lname,
                            email = user.email,
                            password = user.password,
                            phone = user.phone,
                            images = user.images,
                            created_at = DateTime.Now,
                            updated_at = DateTime.Now

                        };
                        db.users.Add(acounnt);
                        db.SaveChanges();
                    }
                    return RedirectToRoute("Login");
                }
                else
                {
                    ViewData["msg"] = "Email In Use";
                    return View("Index");
                }
               
            }
            else
            {
                return RedirectToRoute("Home");
            }


        }

        public JsonResult IsPhoneExist(string phone, int? Id)
        {
            var validateName = db.users.FirstOrDefault(x => x.phone == phone && x.id != Id);
            if (validateName != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
       

     
    }
}