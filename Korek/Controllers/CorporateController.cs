using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Korek.Models;

namespace Korek.Controllers
{
    public class CorporateController : Controller
    {
        // GET: Corporate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<Corporate> empList = db.Corporates.ToList<Corporate>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Corporate());
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.Corporates.Where(x => x.CorporateID == id).FirstOrDefault<Corporate>());
                }
            }
        }
    }
}