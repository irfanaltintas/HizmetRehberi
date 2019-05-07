using HizmetRehberi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HizmetRehberi.Controllers
{
    public class AdminPageController : Controller
    {
        HizmetRehberiDB2 db = new HizmetRehberiDB2();
        // GET: AdminPage
        public ActionResult Index()
        {
            var firmalar = db.Firmalars.ToList();
            return View(firmalar);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var login = db.Admins.Where(a => a.Username == admin.Username).SingleOrDefault();
            if (login.Username == admin.Username && login.Password == admin.Password)
            {
                Session["adminid"] = login.AdminID;
                Session["username"] = login.Username;
                return RedirectToAction("Index", "AdminPage");
            }
            else
            {
                ViewBag.warning = "Kullancı Adı veya Şifre Yanlış";
                return View();
            }

        }
    }
}