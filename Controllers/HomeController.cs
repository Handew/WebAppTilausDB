using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebAppTilausDB.Models;

namespace WebAppTilausDB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Out";
            }
            else
            {
                ViewBag.LoggedStatus = "In";
            }

            return View();
        }

        public ActionResult About()
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Out";
            }
            else
            {
                ViewBag.LoggedStatus = "In";
            }

            ViewBag.Message = "Tätä sivustoa päivitetään parhaillaan...";

            return View();
        }

        public ActionResult Contact()
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Out";
            }
            else
            {
                ViewBag.LoggedStatus = "In";
            }

            return View();
        }

        public ActionResult Kirjautuminen()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Kirjautuminen kirjautuminen)
        {
            TilausDBEntities db = new TilausDBEntities();
            var KirjautunutKayttaja = db.Kirjautuminen.SingleOrDefault(x => x.Kayttajatunnus == kirjautuminen.Kayttajatunnus && x.Salasana == kirjautuminen.Salasana);
            if(KirjautunutKayttaja != null)
            {
                ViewBag.LoginMessage = "Onnistunut kirjautuminen";
                ViewBag.LoggedStatus = "IN";
                Session["Kayttajatunnus"] = KirjautunutKayttaja.Kayttajatunnus;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginMessage = "Kirjautuminen epäonnistui";
                ViewBag.LoggedStatus = "OUT";
                kirjautuminen.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Kirjautuminen", kirjautuminen);
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "OUT";
            return RedirectToAction("Index", "Home");
        }
    }
}