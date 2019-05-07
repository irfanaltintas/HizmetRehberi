using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using HizmetRehberi.Models;

namespace HizmetRehberi.Controllers
{
    public class AdminFirmsController : Controller
    {
        HizmetRehberiDB2 db = new HizmetRehberiDB2();
        // GET: AdminFirms
        public ActionResult Index()
        {
            var firms = db.Firmalars.ToList();
            return View(firms);
        }

        // GET: AdminFirms/Details/5
        public ActionResult Details(int id)
        {
            var firma = db.Firmalars.Where(f => f.FirmaID == id).SingleOrDefault();
            return View(firma);
        }

        // GET: AdminFirms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminFirms/Create
        [HttpPost]
        public ActionResult Create(Firmalar firma, HttpPostedFileBase FirmaResim)
        {
            if (ModelState.IsValid)
            {
                if (FirmaResim != null)
                {
                    WebImage img = new WebImage(FirmaResim.InputStream);                  
                    FileInfo resiminfo = new FileInfo(FirmaResim.FileName);                  
                    string newresim = Guid.NewGuid().ToString() + resiminfo.Extension;               

                    img.Save("~/Uploads/Firmalar/" + newresim);
                    firma.FirmaResim = "/Uploads/Firmalar/" + newresim;                 
                }
                db.Firmalars.Add(firma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(firma);
        }


        // GET: AdminFirms/Edit/5
        public ActionResult Edit(int id)
        {
            var firma = db.Firmalars.Where(f => f.FirmaID == id).SingleOrDefault();
            if (firma == null)
            {
                return HttpNotFound();
            }
            return View(firma);
        }

        // POST: AdminFirms/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase FirmaResim, Firmalar firma)
        {
            try
            {
                var firmalar = db.Firmalars.Where(f => f.FirmaID == id).SingleOrDefault();
                if (FirmaResim != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(firmalar.FirmaResim)))
                    {
                        System.IO.File.Delete(Server.MapPath(firmalar.FirmaResim));
                    }
                    WebImage img = new WebImage(FirmaResim.InputStream);
                    FileInfo resiminfo = new FileInfo(FirmaResim.FileName);
                    string newresim = Guid.NewGuid().ToString() + resiminfo.Extension;

                    img.Save("~/Uploads/Firmalar/" + newresim);
                    firmalar.FirmaResim = "/Uploads/Firmalar/" + newresim;

                    firmalar.FirmaAdi = firma.FirmaAdi;
                    firmalar.FirmaHakkinda = firma.FirmaHakkinda;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminFirms/Delete/5
        public ActionResult Delete(int id)
        {
            var firma = db.Firmalars.Where(m => m.FirmaID == id).SingleOrDefault();
            if (firma == null)
            {
                return HttpNotFound();
            }
            return View(firma);
        }

        // POST: AdminFirms/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var firma = db.Firmalars.Where(m => m.FirmaID == id).SingleOrDefault();
                if (firma == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(firma.FirmaResim)))
                {
                    System.IO.File.Delete(Server.MapPath(firma.FirmaResim));
                }
                foreach (var item in firma.Resimlers.ToList())
                {
                    db.Resimlers.Remove(item);
                }
                db.Firmalars.Remove(firma);
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
