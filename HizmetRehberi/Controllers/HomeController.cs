using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HizmetRehberi.Models;

namespace HizmetRehberi.Controllers
{
    public class HomeController : Controller
    {
        HizmetRehberiDB2 db = new HizmetRehberiDB2();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "Bursa Hizmet Rehberi";
            var firma = db.Firmalars.OrderByDescending(f => f.FirmaID);
            return View(firma);
        }
        public ActionResult Search(string search = null)
        {
            ViewBag.Title = "Ara | Bursa Hizmet Rehberi";
            var searching = db.Firmalars.Where(s => s.FirmaAdi.Contains(search)).ToList();
            return View(searching);
        }

        public ActionResult About()
        {
            ViewBag.Title = "Hakkımızda | Bursa Hizmet Rehberi";
            return View();
        }
        public ActionResult Firms()
        {
            ViewBag.Title = "Firmalar | Bursa Hizmet Rehberi";
            var firma = db.Firmalars.OrderByDescending(f => f.FirmaID);
            return View(firma);
        }
        public ActionResult OneFirm(int id)
        {
            
            var firma = db.Firmalars.Where(f => f.FirmaID == id).SingleOrDefault();
            ViewBag.Title = firma.FirmaAdi + " | Bursa Hizmet Rehberi";
            if (firma == null)
            {
                return HttpNotFound();
            }
            return View(firma);
        }
    }
}