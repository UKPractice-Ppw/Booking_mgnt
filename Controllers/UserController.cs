using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheatreMgnt.Models;

namespace TheatreMgnt.Controllers
{
    public class UserController : Controller
    {
        DBHandler db = new DBHandler();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User us)
        {
                if(db.login_user(us)!=0)
                {
                    Session["User_id"] = db.login_user(us);
                    Session["User_Name"] = us.User_name;
                    return RedirectToAction("Index", "Movie");
                }
                else
                {
                    return RedirectToAction("Index", "Home"); ;  
                }
        }

        // GET: User/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(User u)
        {
            int id = Convert.ToInt32(Session["User_id"]);
            if(db.UpdateUser(id,u))
            {
                return RedirectToAction("Index", "Movie");
            }
            else
            {
                return View();
            }
           
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
