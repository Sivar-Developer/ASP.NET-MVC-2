using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        [HttpPost]
        public ActionResult AddOrEdit(Corporate cor)
        {
            using (DBModel db = new DBModel())
            {
                if (cor.CorporateID == 0)
                {
                    db.Corporates.Add(cor);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Save Successfuly" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(cor).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Update Successfuly" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                Corporate cor = db.Corporates.Where(x => x.CorporateID == id).FirstOrDefault<Corporate>();
                db.Corporates.Remove(cor);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Show(int id = 0)
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

        [HttpGet]
        public ActionResult GetSimcards(int id = 0)
        {
            if (id == 0)
                return View(new Corporate());
            else
            {
                using (DBModel db = new DBModel())
                {
                    List<Simcard> simcards = db.Simcards.Where(x => x.CorporateID == id).ToList<Simcard>();
                    return Json(new { data = simcards }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}