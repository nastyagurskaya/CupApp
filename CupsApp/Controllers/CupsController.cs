using CupsApp.Context;
using CupsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CupsApp.Controllers
{
    public class CupsController : Controller
    {
        // GET: Cups
        private CupContext db = new CupContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewAll()
        {
            return View(GetAllCups());
        }
        IEnumerable<Cup> GetAllCups()
        {
                return db.Cups.ToList<Cup>();
        }
        
        public ActionResult AddOrEdit(int? id)
        {
                Cup c = new Cup();
                ViewBag.CountryID = new SelectList(db.Countries, "CountryId", "CountryName");
                if (id != null)
                {

                    c = db.Cups.Where(x => x.CupId == id).FirstOrDefault<Cup>();
                
                }
            return View(c);
           
        }

        [HttpPost]
        public ActionResult AddOrEdit(Cup cup, HttpPostedFileBase imageUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Cups.Add(cup);
                    db.SaveChanges();
                }
                if (imageUpload != null)
                {
                    CupImage cupImg = new CupImage();
                    cupImg.Image = new byte[imageUpload.ContentLength];
                    imageUpload.InputStream.Read(cupImg.Image, 0, imageUpload.ContentLength);
                    cupImg.Cup = cup;
                    db.CupImages.Add(cupImg);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.CountryID = new SelectList(db.Countries, "CountryId", "CountryName", cup.CountryID);
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllCups()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Cup c = db.Cups.Where(x => x.CupId == id).FirstOrDefault<Cup>();
                db.Cups.Remove(c);
                db.SaveChanges();
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllCups()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetImage(int id)
        {
            CupImage c = db.CupImages.FirstOrDefault(d => d.CupId == id);
            if (c != null &&  c.Image != null) return File(c.Image, "image/png");
            return View();
        }
    }
}