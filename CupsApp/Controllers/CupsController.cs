using CupsApp.Context;
using CupsApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
            //Country ca = db.Cups.Where(x => x.CupId == id).FirstOrDefault<Cup>())
            SelectList items;
            if (id != null)
            {
                c = db.Cups.Find(id);

            }
            ViewBag.CountryID = items = new SelectList(db.Countries, "CountryId", "CountryName", c.CountryID);
            return View(c);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit([Bind(Include = "CupId,Capacity,CupType,CountryID,ImagePath,ImageUpload")]Cup cup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ///var c = db.Cups.Where(x => x.CupId == cup.CupId).FirstOrDefault<Cup>();
                    if (cup.CupId == 0)
                    {
                        db.Cups.Add(cup);
                        db.SaveChanges();
                        if (cup.ImageUpload != null)
                        {
                            CupImage cupImg = new CupImage();
                            cupImg.CupImageID = cup.CupId;
                            cupImg.Image = new byte[cup.ImageUpload.ContentLength];
                            cup.ImageUpload.InputStream.Read(cupImg.Image, 0, cup.ImageUpload.ContentLength);
                          
                            cupImg.Cup = cup;
                            db.CupImages.Add(cupImg);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        if (cup.ImageUpload != null)
                        {
                            var cupImg = db.CupImages.Find(cup.CupId);
                            if (cupImg != null)
                            {
                                cupImg.Image = new byte[cup.ImageUpload.ContentLength];
                                cup.ImageUpload.InputStream.Read(cupImg.Image, 0, cup.ImageUpload.ContentLength);
                                db.Entry(cupImg).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        db.Entry(cup).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                ViewBag.CountryID = new SelectList(db.Countries, "CountryId", "CountryName", cup.CountryID);
                return RedirectToAction("Index");
                //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllCups()), message = "Operation was Successfully" }, JsonRequestBehavior.AllowGet);
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
                var cupImg = db.CupImages.Find(id);
                var c = db.Cups.Where(x => x.CupId == id).FirstOrDefault<Cup>();
                if(cupImg!=null) db.CupImages.Remove(cupImg);
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
            CupImage c = db.CupImages.FirstOrDefault(d => d.CupImageID == id);
            if (c != null &&  c.Image != null) return File(c.Image, "image/png");
            return View();
        }
    }
}