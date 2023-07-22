using newtask2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace newtask2.Controllers
{
    public class RegisterController : Controller
    {
        newtasksEntities1 db = new newtasksEntities1();

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Getdetails()
        {
            using (newtasksEntities1 db = new newtasksEntities1())
            {
                var firstname = db.Userdetails.ToList();
                return Json(new { data = firstname }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]

        public JsonResult Edit(int Id)
        {
            var firstname = db.Userdetails.Where(a => a.Id == Id).FirstOrDefault();
            return Json( firstname, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Remove(int Id)
        {
            bool status = false;
            var firstname = db.Userdetails.FirstOrDefault(a => a.Id == Id);
            if (firstname != null)
            {
                db.Userdetails.Remove(firstname);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult save(Userdetail userdetail)
        {
            bool status = false;

            if (ModelState.IsValid)
            {
                using(newtasksEntities1 db=new newtasksEntities1())
                {
                    if(userdetail.Id>0){
                        var v = db.Userdetails.Where(a => a.Id == userdetail.Id).FirstOrDefault();
                        if (v != null)
                        {                         
                            v.Firstname = userdetail.Firstname;
                            v.Lastname= userdetail.Lastname;
                            v.Username = userdetail.Username;
                            v.Phonenumber = userdetail.Phonenumber;
                            v.Email = userdetail.Email;
                            v.Password = userdetail.Password;
                            db.Entry(v).State = EntityState.Modified;

                        }
                    }
                    else
                        {
                            db.Userdetails.Add(userdetail);
                        }

                      
                    db.SaveChanges();
                    status = true;

                }              
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}