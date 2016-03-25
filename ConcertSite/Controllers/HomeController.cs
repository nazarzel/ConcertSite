using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConcertSite.Models;

namespace ConcertSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Home";

            DataBaseContext Db = new DataBaseContext();
            List<Concert> model = Db.Concerts.OrderByDescending(m => m.time).Take(3).ToList();

            return View(model);
        }

        public ActionResult Gallery()
        {
            ViewBag.Message = "Gallery";

            return View();
        }

        public ActionResult Bilets(int id)
        {
            ViewBag.Message = id.ToString();
            DataBaseContext Db = new DataBaseContext();
            List<Bilet> model = Db.Bilets.Where(u => u.ConcertId == id).ToList();

            return View(model);
        }

        public ActionResult Tours(string s = "")
        {
            DataBaseContext Db = new DataBaseContext();
            List<Concert> model = null;
            if (s == "")
            {
                model = Db.Concerts.ToList();
            }
            else
            {
                model = Db.Concerts.Where(u => u.city.Contains(s) || u.country.Contains(s) || u.singer.Contains(s)).ToList();
            }
            return View(model);
        }
    }
}
