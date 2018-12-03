using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Korek.Models;

namespace Korek.Controllers
{
    public class SimcardController : Controller
    {
        // GET: Simcard
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0, int simId = 0)
        {
            ViewBag.Id = id;
            if (simId == 0)
                return View(new Simcard());
            else
            {
                using (DBModels db = new DBModels())
                {
                    return View(db.Simcards.Where(x => x.SimcardID == simId).Where(x => x.CorporateID == id).FirstOrDefault<Simcard>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Simcard simcard)
        {
            using (DBModels db = new DBModels())
            {
                if (simcard.SimcardID == 0)
                {
                    db.Simcards.Add(simcard);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Save Successfuly" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(simcard).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Update Successfuly" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModels db = new DBModels())
            {
                Simcard simcard = db.Simcards.Where(x => x.CorporateID == id).FirstOrDefault<Simcard>();
                db.Simcards.Remove(simcard);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}