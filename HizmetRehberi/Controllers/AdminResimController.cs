using HizmetRehberi.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HizmetRehberi.Controllers
{
    public class AdminResimController : Controller
    {
        HizmetRehberiDB2 db = new HizmetRehberiDB2();
        // GET: AdminResim
        public ActionResult Index()
        {
            var firma = db.Resimlers.ToList().OrderBy(x=>x.Firmalar.FirmaAdi);
            return View(firma);
        }

        // GET: AdminResim/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminResim/Create
        public ActionResult Create()
        {
            ViewBag.FirmaID = new SelectList(db.Firmalars, "FirmaID", "FirmaAdi");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Resimler resim, IEnumerable<HttpPostedFileBase> resimler)
        {
            if (ModelState.IsValid)
            {
                if (resimler != null) 
                {
                    foreach (var file in resimler)
                    {
                        var fileName = Path.GetFileName(file.FileName);                      
                        var path = Path.Combine(Server.MapPath("~/Uploads/Firmalar/"), file.FileName);
                        file.SaveAs(path);

                        resim.ResimYolu = "/Uploads/Firmalar/" + fileName;
                        db.Resimlers.Add(resim);
                        db.SaveChanges();
                    }

                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resim);
        }

      

        // GET: AdminResim/Delete/5
        public ActionResult Delete(int id)
        {
            var resim = db.Resimlers.Where(r => r.ResimID == id).SingleOrDefault();
            if(resim == null)
            {
                return HttpNotFound();
            }
            return View(resim);
        }

        // POST: AdminResim/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var resim = db.Resimlers.Where(r => r.ResimID == id).SingleOrDefault();
                if (resim == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(resim.ResimYolu)))
                {
                    System.IO.File.Delete(Server.MapPath(resim.ResimYolu));
                }
                db.Resimlers.Remove(resim);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
