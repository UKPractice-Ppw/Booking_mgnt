using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using TheatreMgnt.Models;

namespace TheatreMgnt.Controllers
{
    public class BookingController : Controller
    {
        DBHandler db = new DBHandler();
        private List<SelectListItem> GetCategoryList()
        {
           var ct_list= db.GetAllCategories();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var ct in ct_list)
            {
                list.Add(new SelectListItem { Value = ct.Cat_id.ToString(), Text = ct.Cat_type });
            }
            //ViewBag.CategoryList = list;
            return list;
        }

        public JsonResult GetMoviesByCategory(int catId)
        {
            List<object> list = new List<object>();
            DataTable dt = db.ddlQuery("SELECT Movie_id, Movie_name, Movie_rate FROM tbl_movie WHERE Cat_id = " + catId);

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new
                {
                    Value = dr["Movie_ID"].ToString(),
                    Text = dr["Movie_name"].ToString(),
                    Rate = dr["Movie_rate"].ToString()
                });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: Booking
        public ActionResult Index()
        {
            ViewBag.CatList = GetCategoryList();
            List<Booking> bookingList = db.GetBookings();
            return View(bookingList);
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            int currentUserId = Session["User_id"] != null ? Convert.ToInt32(Session["User_id"]) : 1;

            Booking model = new Booking
            {
                User_id = currentUserId,
                Categories = GetCategoryList(),
                Movies = new List<SelectListItem>()
            };
            return View(model);
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(Booking booking)
        {
            try
            {
                int uid = Session["uid"] != null ? Convert.ToInt32(Session["uid"]) : 1;

                // Fetch accurate rate from DB and calculate total amount
                int rate = db.get_rate(booking.Movie_id);
                int calculatedAmount = rate * booking.No_of_tickets;

                if (db.addBooking(booking, uid, calculatedAmount))
                {
                    return RedirectToAction("Index", "Movie");
                }
            }
            catch
            {
                ViewBag.category = GetCategoryList();
            }
            booking.Categories = GetCategoryList();
            booking.Movies = new List<SelectListItem>();
            return View(booking);
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Booking/Edit/5
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

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Booking/Delete/5
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
