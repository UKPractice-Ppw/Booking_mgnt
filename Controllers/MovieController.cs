using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using TheatreMgnt.Models;

namespace TheatreMgnt.Controllers
{
    public class MovieController : Controller
    {
        DBHandler db = new DBHandler();

        private List<SelectListItem> GetCategoryList()
        {
            string catString = "SELECT * FROM tbl_movie_cat";
            DataTable dt = new DataTable();
            dt = db.ddlQuery(catString);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new SelectListItem { Text = Convert.ToString(row.ItemArray[1]), Value = Convert.ToString(row.ItemArray[0]) });
            }
            //ViewBag.CategoryList = list;
            return list;
        }


        // GET: Movie
        public ActionResult Index()
        {
            ModelState.Clear();
            ViewBag.Message = Session["User_Name"];
            ViewBag.UserID = Session["User_id"];
            return View(db.GetAllMovies());
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movie/Create
        public ActionResult Create(FormCollection form)
        {
            ViewBag.CategoryList = GetCategoryList();

            return View();

        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(Movie mv)
        {
            try
            {
                if (db.add_movie(mv))
                {
                    return RedirectToAction("Index");
                }
                //else
                //{
                //    return View();
                //}
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.CategoryList = GetCategoryList();
                return View(mv);
            }

        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            
            return View();
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,Movie mv)
        {
            try
            {
                if (db.DeleteMovie(id))
                {
                    ViewBag.Message = "Movie deleted successfully.";
                }
                else
                {
                    ViewBag.Message = "Failed to delete movie.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
